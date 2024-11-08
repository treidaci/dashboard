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
        Assert.Equal(playerActivity.Status, PlayerActivityStatus.Suspicious);
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
        Assert.Equal(playerActivity.Status, PlayerActivityStatus.Suspicious);
        Assert.Null(playerActivity.Reason);
    }
    
    [Fact]
    public void MarkAsLegitimate_ShouldSetStatusToLegitimateAndSetReason()
    {
        // Arrange
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);
        const string reason = "Seems legitimate";

        // Act
        playerActivity.MarkAsLegitimate(reason);

        // Assert
        Assert.Equal(playerActivity.Status, PlayerActivityStatus.Legitimate);
        Assert.Equal(reason, playerActivity.Reason);
    }

    [Fact]
    public void MarkAsLegitimate_AllowsNullReason()
    {
        // Arrange
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        // Act
        playerActivity.MarkAsLegitimate(null);

        // Assert
        Assert.Equal(playerActivity.Status, PlayerActivityStatus.Legitimate);
        Assert.Null(playerActivity.Reason);
    }
    
    [Fact]
    public void MarkAsMalicious_ShouldSetStatusToMaliciousAndSetReason()
    {
        // Arrange
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);
        const string reason = "Seems malicious";

        // Act
        playerActivity.MarkAsMalicious(reason);

        // Assert
        Assert.Equal(playerActivity.Status, PlayerActivityStatus.Malicious);
        Assert.Equal(reason, playerActivity.Reason);
    }

    [Fact]
    public void MarkAsMalicious_AllowsNullReason()
    {
        // Arrange
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        // Act
        playerActivity.MarkAsMalicious(null);

        // Assert
        Assert.Equal(playerActivity.Status, PlayerActivityStatus.Malicious);
        Assert.Null(playerActivity.Reason);
    }
}