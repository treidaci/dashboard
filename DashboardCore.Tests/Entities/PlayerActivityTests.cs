using DashboardCore.Entities;

namespace DashboardCore.Tests.Entities;

public class PlayerActivityTests
{
    [Fact]
    public void PlayerActivity_CanBeInstantiatedWithParameters()
    {
        // Arrange
        var id = "1";
        var playerId = "Player123";
        var action = "Move";
        var timestamp = DateTime.UtcNow;
        var isSuspicious = false;

        // Act
        var activity = new PlayerActivity(id, playerId, action, timestamp, isSuspicious);

        // Assert
        Assert.NotNull(activity);
    }

    [Fact]
    public void PlayerActivity_PropertiesAreSetCorrectly()
    {
        // Arrange
        var id = "1";
        var playerId = "Player123";
        var action = "Move";
        var timestamp = DateTime.UtcNow;
        var isSuspicious = false;

        // Act
        var activity = new PlayerActivity(id, playerId, action, timestamp, isSuspicious);

        // Assert
        Assert.Equal(id, activity.Id);
        Assert.Equal(playerId, activity.PlayerId);
        Assert.Equal(action, activity.Action);
        Assert.Equal(timestamp, activity.Timestamp);
        Assert.Equal(isSuspicious, activity.IsSuspicious);
        Assert.Null(activity.Reason);  // Reason should be null by default
    }

    [Fact]
    public void PlayerActivity_Timestamp_ShouldBeUtc()
    {
        // Arrange
        var id = "1";
        var playerId = "Player123";
        var action = "Move";
        var timestamp = DateTime.UtcNow;
        var isSuspicious = false;

        // Act
        var activity = new PlayerActivity(id, playerId, action, timestamp, isSuspicious);

        // Assert
        Assert.Equal(DateTimeKind.Utc, activity.Timestamp.Kind);
    }

    [Fact]
    public void PlayerActivity_DefaultIsSuspicious_ShouldBeFalseIfSetToFalse()
    {
        // Arrange
        var activity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        // Assert
        Assert.False(activity.IsSuspicious, "IsSuspicious should be false when set to false in the constructor.");
        Assert.Null(activity.Reason);  // Reason should be null initially
    }

    [Fact]
    public void MarkAsSuspicious_SetsIsSuspiciousToTrueAndSetsReason()
    {
        // Arrange
        var activity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);
        var reason = "Repeated identical actions";

        // Act
        activity.MarkAsSuspicious(reason);

        // Assert
        Assert.True(activity.IsSuspicious, "IsSuspicious should be set to true after calling MarkAsSuspicious.");
        Assert.Equal(reason, activity.Reason);
    }

    [Fact]
    public void MarkAsSuspicious_AllowsNullReason()
    {
        // Arrange
        var activity = new PlayerActivity("1", "Player123", "Move", DateTime.UtcNow);

        // Act
        activity.MarkAsSuspicious(null);

        // Assert
        Assert.True(activity.IsSuspicious, "IsSuspicious should be set to true after calling MarkAsSuspicious.");
        Assert.Null(activity.Reason);
    }
}