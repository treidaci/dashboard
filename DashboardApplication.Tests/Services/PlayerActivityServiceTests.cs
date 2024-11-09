using DashboardApplication.DTOs;
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
            new PlayerActivity("2", "Player123", "Jump", DateTime.UtcNow, PlayerActivityStatus.Legitimate, "Legitimate")
        };
        
        _mockRepository.Setup(repo => repo.GetActivitiesByPlayerId(playerId))
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
            Assert.Equal(activities[i].Status.ToString(), result.Activities[i].Status);
            Assert.Equal(activities[i].Reason, result.Activities[i].Reason);
        }
    }

    [Fact]
    public async Task ListPlayerActivities_ShouldReturnEmptyListWhenNoActivitiesFound()
    {
        // Arrange
        var playerId = "Player123";
        _mockRepository.Setup(repo => repo.GetActivitiesByPlayerId(playerId))
            .ReturnsAsync(new List<PlayerActivity>()); // No activities

        // Act
        var result = await _service.ListPlayerActivities(playerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(playerId, result.PlayerId);
        Assert.Empty(result.Activities);
    }
    
    [Fact]
    public async Task CreatePlayerActivity_ShouldCallAddPlayerActivityAsync()
    {
        // Arrange
        var playerId = "Player123";
        var createPlayerActivityDto = new CreatePlayerActivityDto
        {
            Action = "Move",
            Timestamp = DateTime.UtcNow
        };

        // Act
        await _service.CreatePlayerActivity(playerId, createPlayerActivityDto);

        // Assert
        _mockRepository.Verify(
            repo => repo.AddActivity(It.Is<PlayerActivity>(
                activity => activity.PlayerId == playerId &&
                            activity.Action == createPlayerActivityDto.Action &&
                            activity.Timestamp == createPlayerActivityDto.Timestamp
            )),
            Times.Once
        );
    }
    
    [Fact]
    public async Task CreatePlayerActivity_ShouldReturnPlayerActivityId()
    {
        // Arrange
        var playerId = "Player123";
        var createPlayerActivityDto = new CreatePlayerActivityDto
        {
            Action = "Move",
            Timestamp = DateTime.UtcNow
        };

        // Act
        var id = await _service.CreatePlayerActivity(playerId, createPlayerActivityDto);
        
        // Assert
        Assert.NotNull(id);
    }
}