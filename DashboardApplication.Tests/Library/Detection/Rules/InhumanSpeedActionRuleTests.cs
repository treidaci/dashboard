using DashboardApplication.Library.Detection.Rules;
using DashboardCore.Entities;
using DashboardCore.Repositories;
using Moq;

namespace DashboardApplication.Tests.Library.Detection.Rules;

public class InhumanSpeedActionRuleTests
{
    private readonly TimeSpan _inhumanSpeedThreshold = TimeSpan.FromMilliseconds(100);

    [Fact]
    public async Task ApplyRule_ShouldReturnTrue_WhenInhumanSpeedActionExists()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var rule = new InhumanSpeedActionRule(repositoryMock.Object);

        var activity = new PlayerActivity(
            id: "1",
            playerId: "Player123",
            action: "Move",
            timestamp: DateTime.UtcNow
        );

        // Set up the repository to return an activity within 100 milliseconds
        repositoryMock
            .Setup(repo => repo.GetActivitiesWithinThreshold(activity, _inhumanSpeedThreshold))
            .ReturnsAsync(new List<PlayerActivity>
            {
                new(
                    id: "2",
                    playerId: "Player123",
                    action: "Move",
                    timestamp: activity.Timestamp.AddMilliseconds(-50) // Within 100ms threshold
                )
            });

        // Act
        var result = await rule.Apply(activity);

        // Assert
        Assert.True(result, "Rule should apply when an inhuman speed action exists.");
    }

    [Fact]
    public async Task ApplyRule_ShouldReturnFalse_WhenNoInhumanSpeedActionExists()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var rule = new InhumanSpeedActionRule(repositoryMock.Object);

        var activity = new PlayerActivity(
            id: "1",
            playerId: "Player123",
            action: "Move",
            timestamp: DateTime.UtcNow
        );

        // Set up the repository to return no activities within 100 milliseconds
        repositoryMock
            .Setup(repo => repo.GetActivitiesWithinThreshold(activity, _inhumanSpeedThreshold))
            .ReturnsAsync(new List<PlayerActivity>());

        // Act
        var result = await rule.Apply(activity);

        // Assert
        Assert.False(result, "Rule should not apply when no inhuman speed action exists.");
    }

    [Fact]
    public async Task Reason_ShouldReturnCorrectReason()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var rule = new InhumanSpeedActionRule(repositoryMock.Object);

        var activity = new PlayerActivity(
            id: "1",
            playerId: "Player123",
            action: "Move",
            timestamp: DateTime.UtcNow
        );
        
        // Set up the repository to return an activity within 100 milliseconds
        repositoryMock
            .Setup(repo => repo.GetActivitiesWithinThreshold(activity, _inhumanSpeedThreshold))
            .ReturnsAsync(new List<PlayerActivity>
            {
                new(
                    id: "2",
                    playerId: "Player123",
                    action: "Move",
                    timestamp: activity.Timestamp.AddMilliseconds(-50) // Within 100ms threshold
                )
            });

        // Act
        await rule.Apply(activity);

        // Assert
        Assert.Equal(PlayerActivityStatus.Suspicious, activity.Status);
        Assert.Equal("Inhuman speed", activity.Reason);
    }
}