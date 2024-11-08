using DashboardApplication.Library.Detection;
using DashboardApplication.Library.Detection.Rules;
using DashboardCore.Entities;
using DashboardCore.Repositories;
using Moq;

namespace DashboardApplication.Tests.Library.Detection;

public class DetectionServiceTests
{
    [Fact]
    public async Task AnalysePlayerActivity_ShouldApplyRulesAndUpdateActivity_WhenAnyRuleIsApplied()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var ruleMock1 = new Mock<IDetectionRule>();
        var ruleMock2 = new Mock<IDetectionRule>();
        var ruleMock3 = new Mock<IDetectionRule>();

        var activity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        repositoryMock.Setup(repo => repo.GetActivity("1", "Player123"))
            .ReturnsAsync(activity);

        // Set up the rules to return true for Apply, meaning the rule has been applied
        ruleMock1.Setup(rule => rule.Apply(activity)).ReturnsAsync(false);
        ruleMock2.Setup(rule => rule.Apply(activity)).ReturnsAsync(true);
        ruleMock3.Setup(rule => rule.Apply(activity)).ReturnsAsync(false);

        var detectionService = new DetectionService(repositoryMock.Object, [ ruleMock1.Object, ruleMock2.Object, ruleMock3.Object ]);

        // Act
        await detectionService.AnalysePlayerActivity("1", "Player123");

        // Assert
        ruleMock1.Verify(rule => rule.Apply(activity), Times.Once, "Rule 1 should be applied to the activity");
        ruleMock2.Verify(rule => rule.Apply(activity), Times.Once, "Rule 2 should be applied to the activity");
        ruleMock3.Verify(rule => rule.Apply(activity), Times.Once, "Rule 3 should be applied to the activity");
        repositoryMock.Verify(repo => repo.UpdateActivity(activity), Times.Once, "UpdateActivity should be called when any rule is applied");
    }

    [Fact]
    public async Task AnalysePlayerActivity_ShouldNotUpdateActivity_WhenNoRulesAreApplied()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var ruleMock1 = new Mock<IDetectionRule>();
        var ruleMock2 = new Mock<IDetectionRule>();

        var activity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        repositoryMock.Setup(repo => repo.GetActivity("1", "Player123"))
            .ReturnsAsync(activity);

        // Set up the rules to return false for Apply, meaning no rules were applied
        ruleMock1.Setup(rule => rule.Apply(activity)).ReturnsAsync(false);
        ruleMock2.Setup(rule => rule.Apply(activity)).ReturnsAsync(false);

        var detectionService = new DetectionService(repositoryMock.Object, [ ruleMock1.Object, ruleMock2.Object ]);

        // Act
        await detectionService.AnalysePlayerActivity("1", "Player123");

        // Assert
        ruleMock1.Verify(rule => rule.Apply(activity), Times.Once, "Rule 1 should be applied to the activity");
        ruleMock2.Verify(rule => rule.Apply(activity), Times.Once, "Rule 2 should be applied to the activity");
        repositoryMock.Verify(repo => repo.UpdateActivity(It.IsAny<PlayerActivity>()), Times.Never, "UpdateActivity should not be called if no rules are applied");
    }

    [Fact]
    public async Task AnalysePlayerActivity_ShouldReturn_WhenActivityDoesNotExist()
    {
        // Arrange
        var repositoryMock = new Mock<IPlayerActivityRepository>();
        var ruleMock1 = new Mock<IDetectionRule>();
        var ruleMock2 = new Mock<IDetectionRule>();

        // Set up the repository to return null, simulating that the activity does not exist
        repositoryMock.Setup(repo => repo.GetActivity("NonExistentId", "Player123"))
            .ReturnsAsync((PlayerActivity?)null);

        var detectionService = new DetectionService(repositoryMock.Object, [ ruleMock1.Object, ruleMock2.Object ]);

        // Act
        await detectionService.AnalysePlayerActivity("NonExistentId", "Player123");

        // Assert
        repositoryMock.Verify(repo => repo.GetActivity("NonExistentId", "Player123"), Times.Once, "GetActivity should be called once");
        ruleMock1.Verify(rule => rule.Apply(It.IsAny<PlayerActivity>()), Times.Never, "No rule should be applied if the activity does not exist");
        ruleMock2.Verify(rule => rule.Apply(It.IsAny<PlayerActivity>()), Times.Never, "No rule should be applied if the activity does not exist");
        repositoryMock.Verify(repo => repo.UpdateActivity(It.IsAny<PlayerActivity>()), Times.Never, "UpdateActivity should not be called if the activity does not exist");
    }
}