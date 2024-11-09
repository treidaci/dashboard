using DashboardCore.Entities;

namespace DashboardCore.Tests.Entities;

public class PlayerActivityTests
{
    [Fact]
    public void PlayerActivity_ConstructorPrimaryConstructor_ShouldSetProperties()
    {
        // Arrange
        var id = "1";
        var playerId = "Player123";
        var action = "Move";
        var timestamp = DateTime.UtcNow;

        // Act
        var playerActivity = new PlayerActivity(id, playerId, action, timestamp);

        // Assert
        Assert.Equal(id, playerActivity.Id);
        Assert.Equal(playerId, playerActivity.PlayerId);
        Assert.Equal(action, playerActivity.Action);
        Assert.Equal(timestamp, playerActivity.Timestamp);
    }

    [Fact]
    public void PlayerActivity_DefaultStatus_ShouldBeLegitimate()
    {
        // Arrange & Act
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        // Assert
        Assert.Equal(PlayerActivityStatus.Legitimate, playerActivity.Status);
        Assert.Null(playerActivity.Reason);
    }

    [Fact]
    public void PlayerActivity_Timestamp_ShouldBeUtc()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;

        // Act
        var playerActivity = new PlayerActivity("1", "Player123", "Move", timestamp);

        // Assert
        Assert.Equal(DateTimeKind.Utc, playerActivity.Timestamp.Kind);
    }

    [Fact]
    public void PlayerActivity_SecondaryConstructor_ShouldSetProperties()
    {
        // Arrange
        var id = "1";
        var playerId = "Player123";
        var action = "Move";
        var timestamp = DateTime.UtcNow;
        var reason = "Reason";

        // Act
        var playerActivity = new PlayerActivity(id, playerId, action, timestamp, PlayerActivityStatus.Malicious, reason);

        // Assert
        Assert.Equal(id, playerActivity.Id);
        Assert.Equal(playerId, playerActivity.PlayerId);
        Assert.Equal(action, playerActivity.Action);
        Assert.Equal(timestamp, playerActivity.Timestamp);
        Assert.Equal(PlayerActivityStatus.Malicious, playerActivity.Status);
        Assert.Equal(reason, playerActivity.Reason);
    }
    
    // separate tests per MarkAsXXX method to avoid conditional logic in tests
    [Fact]
    public void MarkAsSuspicious_ShouldSetStatusToSuspiciousAndSetReason()
    {
        // Arrange
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);
        const string reason = "Suspicious activity detected";

        // Act
        playerActivity.MarkAsSuspicious(reason);

        // Assert
        Assert.Equal(PlayerActivityStatus.Suspicious, playerActivity.Status);
        Assert.Equal(reason, playerActivity.Reason);
    }

    [Fact]
    public void MarkAsSuspicious_AllowsNullReason()
    {
        // Arrange
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        // Act
        playerActivity.MarkAsSuspicious(null);

        // Assert
        Assert.Equal(PlayerActivityStatus.Suspicious, playerActivity.Status);
        Assert.Null(playerActivity.Reason);
    }
    
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly_WithValidStatusString()
    {
        // Arrange
        var id = "1";
        var playerId = "Player123";
        var action = "Move";
        var timestamp = DateTime.UtcNow;
        var status = "Suspicious";
        var reason = "Repeated action";

        // Act
        var activity = new PlayerActivity(id, playerId, action, timestamp, status, reason);

        // Assert
        Assert.Equal(id, activity.Id);
        Assert.Equal(playerId, activity.PlayerId);
        Assert.Equal(action, activity.Action);
        Assert.Equal(timestamp, activity.Timestamp);
        Assert.Equal(PlayerActivityStatus.Suspicious, activity.Status);
        Assert.Equal(reason, activity.Reason);
    }

    [Fact]
    public void Constructor_ShouldSetStatusToLegitimate_WhenStatusStringIsLegitimate()
    {
        // Arrange
        var id = "2";
        var playerId = "Player456";
        var action = "Run";
        var timestamp = DateTime.UtcNow;
        var status = "Legitimate";
        string reason = null;

        // Act
        var activity = new PlayerActivity(id, playerId, action, timestamp, status, reason);

        // Assert
        Assert.Equal(id, activity.Id);
        Assert.Equal(playerId, activity.PlayerId);
        Assert.Equal(action, activity.Action);
        Assert.Equal(timestamp, activity.Timestamp);
        Assert.Equal(PlayerActivityStatus.Legitimate, activity.Status);
        Assert.Null(activity.Reason);
    }

    [Fact]
    public void Constructor_ShouldSetStatusToMalicious_WhenStatusStringIsMalicious()
    {
        // Arrange
        var id = "3";
        var playerId = "Player789";
        var action = "Attack";
        var timestamp = DateTime.UtcNow;
        var status = "Malicious";
        var reason = "Unauthorized action";

        // Act
        var activity = new PlayerActivity(id, playerId, action, timestamp, status, reason);

        // Assert
        Assert.Equal(id, activity.Id);
        Assert.Equal(playerId, activity.PlayerId);
        Assert.Equal(action, activity.Action);
        Assert.Equal(timestamp, activity.Timestamp);
        Assert.Equal(PlayerActivityStatus.Malicious, activity.Status);
        Assert.Equal(reason, activity.Reason);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenStatusStringIsInvalid()
    {
        // Arrange
        var id = "4";
        var playerId = "Player987";
        var action = "Defend";
        var timestamp = DateTime.UtcNow;
        var invalidStatus = "InvalidStatus";
        var reason = "Unrecognized status";

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            new PlayerActivity(id, playerId, action, timestamp, invalidStatus, reason));

        Assert.Contains("Requested value 'InvalidStatus' was not found", exception.Message);
    }
}