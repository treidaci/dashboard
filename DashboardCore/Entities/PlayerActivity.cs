namespace DashboardCore.Entities;

public class PlayerActivity(string id, string playerId, string action, DateTime timestamp)
{
    public PlayerActivity(string id, string playerId, string action, DateTime timestamp, PlayerActivityStatus status, string? reason) : this(id, playerId, action, timestamp)
    {
        Status = status;
        Reason = reason;
    }
    public string Id { get; } = id;
    public string PlayerId { get; } = playerId;
    public string Action { get; } = action;
    public DateTime Timestamp { get; } = timestamp;

    public PlayerActivityStatus Status { get; private set; } = PlayerActivityStatus.Legitimate;

    public string? Reason { get; private set; }

    public void MarkAsSuspicious(string? reason)
    {
        Status = PlayerActivityStatus.Suspicious;
        // on all of these methods, we could append the reason so
        // we could capture a history of why the activity is suspicious
        Reason = reason;
    }
    public void MarkAsMalicious(string? reason)
    {
        Status = PlayerActivityStatus.Malicious;
        Reason = reason;
    }
    public void MarkAsLegitimate(string? reason)
    {
        Status = PlayerActivityStatus.Legitimate;
        Reason = reason;
    }
}