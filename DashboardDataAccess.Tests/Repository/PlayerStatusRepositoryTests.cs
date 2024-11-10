using DashboardCore.Entities;
using DashboardDataAccess;
using DashboardDataAccess.Models;
using DashboardDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DashboardDataAccessTests.Repository;

public class PlayerStatusRepositoryTests
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
        var statuses = new[]
        {
            new PlayerStatusDb { PlayerId = "Player123", Status = "Active", Reason = "Player is active" },
            new PlayerStatusDb { PlayerId = "Player456", Status = "Suspicious", Reason = "Suspicious activity detected" }
        };

        await context.PlayerStatuses.AddRangeAsync(statuses);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetPlayerStatus_ShouldReturnPlayerStatus_WhenStatusExists()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerStatusRepository(context);

        // Act
        var result = await repository.GetPlayerStatus("Player123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Player123", result.PlayerId);
        Assert.Equal(PlayerStatusType.Active, result.Status);
        Assert.Equal("Player is active", result.Reason);
    }

    [Fact]
    public async Task GetPlayerStatus_ShouldReturnNull_WhenStatusDoesNotExist()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerStatusRepository(context);

        // Act
        var result = await repository.GetPlayerStatus("NonExistentPlayer");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreatePlayerStatus_ShouldAddNewStatusToDatabase()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerStatusRepository(context);

        var newStatus = new PlayerStatus("Player789", "Banned", "Player is banned");

        // Act
        await repository.CreatePlayerStatus(newStatus);

        // Assert
        var savedStatus = await context.PlayerStatuses.FirstOrDefaultAsync(ps => ps.PlayerId == "Player789");
        Assert.NotNull(savedStatus);
        Assert.Equal("Player789", savedStatus.PlayerId);
        Assert.Equal("Banned", savedStatus.Status);
        Assert.Equal("Player is banned", savedStatus.Reason);
    }

    [Fact]
    public async Task UpdatePlayerStatus_ShouldUpdateExistingStatusInDatabase()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerStatusRepository(context);

        var updatedStatus = new PlayerStatus("Player456", "Banned", "Player is banned due to multiple violations");

        // Act
        await repository.UpdatePlayerStatus(updatedStatus);

        // Assert
        var savedStatus = await context.PlayerStatuses.FirstOrDefaultAsync(ps => ps.PlayerId == "Player456");
        Assert.NotNull(savedStatus);
        Assert.Equal("Banned", savedStatus.Status);
        Assert.Equal("Player is banned due to multiple violations", savedStatus.Reason);
    }

    [Fact]
    public async Task UpdatePlayerStatus_ShouldNotThrow_WhenStatusDoesNotExist()
    {
        // Arrange
        var context = await GetInMemoryDbContext();
        var repository = new PlayerStatusRepository(context);

        var nonExistentStatus = new PlayerStatus("NonExistentPlayer", "Suspicious", "Test reason");

        // Act
        await repository.UpdatePlayerStatus(nonExistentStatus);

        // Assert
        // Ensure no status was added to the database
        var savedStatus = await context.PlayerStatuses.FirstOrDefaultAsync(ps => ps.PlayerId == "NonExistentPlayer");
        Assert.Null(savedStatus);
    }
}