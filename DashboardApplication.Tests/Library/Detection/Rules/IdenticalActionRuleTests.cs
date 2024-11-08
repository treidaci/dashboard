using System.Linq.Expressions;
using DashboardApplication.Library.Detection.Rules;
using DashboardCore.Entities;
using DashboardCore.Repositories;
using Moq;

namespace DashboardApplication.Tests.Library.Detection.Rules;

public class IdenticalActionRuleTests
{
    [Fact]
    public async Task Apply_ShouldMarkActivityAsSuspicious_WhenIdenticalActionExists()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var rule = new IdenticalActionRule(repositoryMock.Object);

        var activity = new PlayerActivity(
            id: "1",
            playerId: "Player123",
            action: "Move",
            timestamp: DateTime.UtcNow
        );

        // Set up the repository to return a list with one identical activity
        repositoryMock
            .Setup(repo => repo.GetActivitiesByFilter(It.IsAny<Expression<Func<PlayerActivity, bool>>>()))
            .ReturnsAsync(new List<PlayerActivity> { activity });

        // Act
        var result = await rule.Apply(activity);

        // Assert
        Assert.True(result, "Rule should apply when an identical action exists.");
        Assert.Equal(PlayerActivityStatus.Suspicious, activity.Status);
        Assert.Equal("Identical action", activity.Reason);
    }

    [Fact]
    public async Task Apply_ShouldNotMarkActivityAsSuspicious_WhenNoIdenticalActionExists()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var rule = new IdenticalActionRule(repositoryMock.Object);

        var activity = new PlayerActivity(
            id: "1",
            playerId: "Player123",
            action: "Move",
            timestamp: DateTime.UtcNow
        );

        // Set up the repository to return an empty list, meaning no identical activities were found
        repositoryMock
            .Setup(repo => repo.GetActivitiesByFilter(It.IsAny<Expression<Func<PlayerActivity, bool>>>()))
            .ReturnsAsync(new List<PlayerActivity>());

        // Act
        var result = await rule.Apply(activity);

        // Assert
        Assert.False(result, "Rule should not apply when no identical action exists.");
        Assert.Equal(PlayerActivityStatus.Legitimate, activity.Status);
        Assert.Null(activity.Reason);
    }

    [Fact]
    public async Task Apply_ShouldUseCorrectReason_WhenRuleIsApplied()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var rule = new IdenticalActionRule(repositoryMock.Object);

        var activity = new PlayerActivity(
            id: "1",
            playerId: "Player123",
            action: "Move",
            timestamp: DateTime.UtcNow
        );

        // Set up the repository to return a list with one identical activity
        repositoryMock
            .Setup(repo => repo.GetActivitiesByFilter(It.IsAny<Expression<Func<PlayerActivity, bool>>>()))
            .ReturnsAsync(new List<PlayerActivity> { activity });

        // Act
        await rule.Apply(activity);

        // Assert
        Assert.Equal(PlayerActivityStatus.Suspicious, activity.Status);
        Assert.Equal("Identical action", activity.Reason);
    }
    
}