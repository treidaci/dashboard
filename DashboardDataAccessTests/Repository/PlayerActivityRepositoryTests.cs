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
            new() { Id = "1", PlayerId = "Player123", Action = "Move", Timestamp = DateTime.UtcNow, IsSuspicious = false, Reason = null },
            new() { Id = "2", PlayerId = "Player123", Action = "Jump", Timestamp = DateTime.UtcNow, IsSuspicious = true, Reason = "Inhuman speed" },
            new() { Id = "3", PlayerId = "Player456", Action = "Run", Timestamp = DateTime.UtcNow, IsSuspicious = false, Reason = null }
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
        var result = await repository.GetActivitiesByPlayerIdAsync("Player123");

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
        var result = await repository.GetActivitiesByPlayerIdAsync("NonExistentPlayer");

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
        var result = await repository.GetActivitiesByPlayerIdAsync("Player123");

        // Assert
        Assert.NotNull(result);
        var activity = result.FirstOrDefault(a => a.Id == "2");
        Assert.NotNull(activity);
        Assert.Equal("Jump", activity.Action);
        Assert.True(activity.IsSuspicious);
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
        await repository.AddPlayerActivityAsync(playerActivity);

        // Assert
        var savedActivity = await context.PlayerActivities.FirstOrDefaultAsync(a => a.Id == "4");
        Assert.NotNull(savedActivity);
        Assert.Equal(playerActivity.PlayerId, savedActivity.PlayerId);
        Assert.Equal(playerActivity.Action, savedActivity.Action);
    }
}