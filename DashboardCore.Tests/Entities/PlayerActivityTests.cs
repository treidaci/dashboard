using DashboardCore.Entities;

namespace DashboardCore.Tests.Entities;

public class PlayerActivityTests
{
     [Fact]
    public void PlayerActivity_Constructor_ShouldSetProperties()
    {
        // Arrange
        var id = "1";
        var playerId = "Player123";
        var action = "Move";
        var timestamp = DateTime.UtcNow;
        var isSuspicious = true;
        var reason = "Repeated actions";

        // Act
        var playerActivity = new PlayerActivity(id, playerId, action, timestamp, isSuspicious, reason);

        // Assert
        Assert.Equal(id, playerActivity.Id);
        Assert.Equal(playerId, playerActivity.PlayerId);
        Assert.Equal(action, playerActivity.Action);
        Assert.Equal(timestamp, playerActivity.Timestamp);
        Assert.Equal(isSuspicious, playerActivity.IsSuspicious);
        Assert.Equal(reason, playerActivity.Reason);
    }

    [Fact]
    public void PlayerActivity_DefaultIsSuspicious_ShouldBeFalse()
    {
        // Arrange & Act
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        // Assert
        Assert.False(playerActivity.IsSuspicious);
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
    public void MarkAsSuspicious_ShouldSetIsSuspiciousToTrueAndSetReason()
    {
        // Arrange
        var playerActivity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);
        var reason = "Suspicious activity detected";

        // Act
        playerActivity.MarkAsSuspicious(reason);

        // Assert
        Assert.True(playerActivity.IsSuspicious);
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
        Assert.True(playerActivity.IsSuspicious);
        Assert.Null(playerActivity.Reason);
    }
}