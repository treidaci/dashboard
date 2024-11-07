using DashboardApplication.Services;
using DashboardCore.Entities;
using DashboardCore.Repositories;
using Moq;

namespace DashboardApplication.Tests.Services;

public class PlayerActivityServiceTests
{
    private readonly Mock<IPlayerActivityRepository> _mockRepository;
    private readonly PlayerActivityService _service;

    public PlayerActivityServiceTests()
    {
        _mockRepository = new Mock<IPlayerActivityRepository>();
        _service = new PlayerActivityService(_mockRepository.Object);
    }

    [Fact]
    public async Task ListPlayerActivities_ShouldReturnPlayerActivityListDto()
    {
        // Arrange
        var playerId = "Player123";
        var activities = new List<PlayerActivity>
        {
            new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow),
            new PlayerActivity("2", "Player123", "Jump", DateTime.UtcNow, true)
        };
        
        _mockRepository.Setup(repo => repo.GetActivitiesByPlayerIdAsync(playerId))
            .ReturnsAsync(activities);

        // Act
        var result = await _service.ListPlayerActivities(playerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(playerId, result.PlayerId);
        Assert.Equal(activities.Count, result.Activities.Count);

        for (var i = 0; i < activities.Count; i++)
        {
            Assert.Equal(activities[i].Id, result.Activities[i].Id);
            Assert.Equal(activities[i].Action, result.Activities[i].Action);
            Assert.Equal(activities[i].Timestamp, result.Activities[i].Timestamp);
            Assert.Equal(activities[i].IsSuspicious, result.Activities[i].IsSuspicious);
            Assert.Equal(activities[i].Reason, result.Activities[i].Reason);
        }
    }

    [Fact]
    public async Task ListPlayerActivities_ShouldReturnEmptyListWhenNoActivitiesFound()
    {
        // Arrange
        var playerId = "Player123";
        _mockRepository.Setup(repo => repo.GetActivitiesByPlayerIdAsync(playerId))
            .ReturnsAsync(new List<PlayerActivity>()); // No activities

        // Act
        var result = await _service.ListPlayerActivities(playerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(playerId, result.PlayerId);
        Assert.Empty(result.Activities);
    }
}