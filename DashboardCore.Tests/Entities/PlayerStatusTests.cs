using DashboardCore.Entities;

namespace DashboardCore.Tests.Entities;

public class PlayerStatusTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly_WithValidStatusString()
    {
        // Arrange
        var playerId = "Player123";
        var status = "Active";
        var reason = "Player is active";

        // Act
        var playerStatus = new PlayerStatus(playerId, status, reason);

        // Assert
        Assert.Equal(playerId, playerStatus.PlayerId);
        Assert.Equal(PlayerStatusType.Active, playerStatus.Status);
        Assert.Equal(reason, playerStatus.Reason);
    }

    [Fact]
    public void Constructor_ShouldSetStatusToSuspicious_WhenStatusStringIsSuspicious()
    {
        // Arrange
        var playerId = "Player456";
        var status = "Suspicious";
        var reason = "Suspicious activity detected";

        // Act
        var playerStatus = new PlayerStatus(playerId, status, reason);

        // Assert
        Assert.Equal(playerId, playerStatus.PlayerId);
        Assert.Equal(PlayerStatusType.Suspicious, playerStatus.Status);
        Assert.Equal(reason, playerStatus.Reason);
    }

    [Fact]
    public void Constructor_ShouldSetStatusToBanned_WhenStatusStringIsBanned()
    {
        // Arrange
        var playerId = "Player789";
        var status = "Banned";
        var reason = "Player is banned due to violations";

        // Act
        var playerStatus = new PlayerStatus(playerId, status, reason);

        // Assert
        Assert.Equal(playerId, playerStatus.PlayerId);
        Assert.Equal(PlayerStatusType.Banned, playerStatus.Status);
        Assert.Equal(reason, playerStatus.Reason);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenStatusStringIsInvalid()
    {
        // Arrange
        var playerId = "Player123";
        var invalidStatus = "UnknownStatus";
        var reason = "Invalid status provided";

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new PlayerStatus(playerId, invalidStatus, reason));

        Assert.Contains("Requested value 'UnknownStatus' was not found", exception.Message);
    }
}