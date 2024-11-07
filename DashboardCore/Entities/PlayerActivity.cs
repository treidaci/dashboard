namespace DashboardCore.Entities;

public class PlayerActivity(string id, string playerId, string action, DateTime timestamp, bool isSuspicious = false, string? reason = null)
{
    public string Id { get; } = id;
    public string PlayerId { get; } = playerId;
    public string Action { get; } = action;
    public DateTime Timestamp { get; } = timestamp;
    public bool IsSuspicious { get; private set; } = isSuspicious;

    public string? Reason { get; private set; } = reason;

    public void MarkAsSuspicious(string? reason)
    {
        IsSuspicious = true;
        Reason = reason;
    }
}