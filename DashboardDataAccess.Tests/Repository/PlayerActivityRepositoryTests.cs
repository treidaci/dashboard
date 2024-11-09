using System.Linq.Expressions;
using DashboardCore.Entities;
using DashboardDataAccess;
using DashboardDataAccess.Models;
using DashboardDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DashboardDataAccessTests.Repository;

public class PlayerActivityRepositoryTests
{
    private async Task<DashboardDbContext> GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<DashboardDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new DashboardDbContext(options);
        await SeedTestData(context);

        return context;
    }

    private async Task SeedTestData(DashboardDbContext context)
    {
        var activities = new List<PlayerActivityDb>
        {
            new() { Id = "1", PlayerId = "Player123", Action = "Move", Timestamp = DateTime.UtcNow, Status = "Suspicious", Reason = null },
            new() { Id = "2", PlayerId = "Player123", Action = "Jump", Timestamp = DateTime.UtcNow, Status = "Legitimate", Reason = "Inhuman speed" },
            new() { Id = "3", PlayerId = "Player456", Action = "Run", Timestamp = DateTime.UtcNow, Status = "Malicious", Reason = null }
        };

        await context.PlayerActivities.AddRangeAsync(activities);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetActivitiesByPlayerIdAsync_ShouldReturnActivitiesForSpecifiedPlayerId()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        // Act
        var result = await repository.GetActivitiesByPlayerId("Player123");

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count); // Two activities for Player123
        Assert.All(resultList, a => Assert.Equal("Player123", a.PlayerId));
    }

    [Fact]
    public async Task GetActivitiesByPlayerIdAsync_ShouldReturnEmptyListWhenNoActivitiesForPlayerId()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        // Act
        var result = await repository.GetActivitiesByPlayerId("NonExistentPlayer");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetActivitiesByPlayerIdAsync_ShouldMapPlayerActivityDbToPlayerActivityCorrectly()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        // Act
        var result = await repository.GetActivitiesByPlayerId("Player123");

        // Assert
        Assert.NotNull(result);
        var activity = result.FirstOrDefault(a => a.Id == "2");
        Assert.NotNull(activity);
        Assert.Equal("Jump", activity.Action);
        Assert.Equal(PlayerActivityStatus.Legitimate, activity.Status);
        Assert.Equal("Inhuman speed", activity.Reason);
    }
    
    [Fact]
    public async Task AddPlayerActivityAsync_ShouldAddPlayerActivityToDatabase()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        var playerActivity = new PlayerActivity(
            id: "4",
            playerId: "Player123",
            action: "Move",
            timestamp: DateTime.UtcNow
        );

        // Act
        await repository.AddActivity(playerActivity);

        // Assert
        var savedActivity = await context.PlayerActivities.FirstOrDefaultAsync(a => a.Id == "4");
        Assert.NotNull(savedActivity);
        Assert.Equal(playerActivity.PlayerId, savedActivity.PlayerId);
        Assert.Equal(playerActivity.Action, savedActivity.Action);
    }
    
    [Fact]
    public async Task GetActivity_ShouldReturnActivity_WhenActivityExists()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);
        
        // Act
        var result = await repository.GetActivity("2", "Player123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("2", result.Id);
        Assert.Equal("Player123", result.PlayerId);
        Assert.Equal("Jump", result.Action);
        Assert.Equal(PlayerActivityStatus.Legitimate, result.Status);
        Assert.Equal("Inhuman speed", result.Reason);
    }

    [Fact]
    public async Task GetActivity_ShouldReturnNull_WhenActivityDoesNotExist()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        // Act
        var result = await repository.GetActivity("NonExistentId", "Player123");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateActivity_ShouldUpdateExistingActivityInDatabase()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        var updatedActivity = new PlayerActivity(
            id: "2",
            playerId: "Player123",
            action: "Climb",
            timestamp: DateTime.UtcNow,
            status: PlayerActivityStatus.Malicious,
            reason: "Repeated suspicious actions"
        );

        // Act
        await repository.UpdateActivity(updatedActivity);

        // Assert
        var savedActivity = await context.PlayerActivities.FirstOrDefaultAsync(a => a.Id == "2" && a.PlayerId == "Player123");
        Assert.NotNull(savedActivity);
        Assert.Equal("Climb", savedActivity.Action);
        Assert.Equal(PlayerActivityStatus.Malicious.ToString(), savedActivity.Status);
        Assert.Equal("Repeated suspicious actions", savedActivity.Reason);
    }

    [Fact]
    public async Task UpdateActivity_ShouldNotThrowException_WhenActivityDoesNotExist()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        var nonExistentActivity = new PlayerActivity(
            id: "NonExistentId",
            playerId: "Player123",
            action: "Fly",
            timestamp: DateTime.UtcNow,
            status: PlayerActivityStatus.Malicious,
            reason: "Non-existent activity"
        );

        // Act
        await repository.UpdateActivity(nonExistentActivity);

        // Assert
        // Ensure that no exception is thrown and the database remains unchanged
        var savedActivity = await context.PlayerActivities.FirstOrDefaultAsync(a => a.Id == "NonExistentId");
        Assert.Null(savedActivity);
    }
    
    [Fact]
    public async Task GetActivitiesByFilter_ShouldReturnFilteredActivities()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        // Define the filter expression
        Expression<Func<PlayerActivity, bool>> filter = a => a.PlayerId == "Player123" && a.Action == "Jump";

        // Act
        var result = await repository.GetActivitiesByFilter(filter);

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList); // Expecting only one match
        Assert.Equal("Player123", resultList[0].PlayerId);
        Assert.Equal("Jump", resultList[0].Action);
        Assert.Equal(PlayerActivityStatus.Legitimate, resultList[0].Status);
        Assert.Equal("Inhuman speed", resultList[0].Reason);
    }

    [Fact]
    public async Task GetActivitiesByFilter_ShouldReturnEmptyList_WhenNoActivitiesMatchFilter()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerActivityRepository(context);

        // Define a filter that will not match any activities
        Expression<Func<PlayerActivity, bool>> filter = a => a.PlayerId == "NonExistentPlayer";

        // Act
        var result = await repository.GetActivitiesByFilter(filter);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result); // Expecting an empty list
    }
}