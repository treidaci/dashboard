namespace DashboardCore.Entities;

public class PlayerActivity(string id, string playerId, string action, DateTime timestamp, bool isSuspicious = false)
{
    public string Id { get; set; } = id;
    public string PlayerId { get; set; } = playerId;
    public string Action { get; set; } = action;
    public DateTime Timestamp { get; set; } = timestamp;
    public bool IsSuspicious { get; set; } = isSuspicious;
    
    public string? Reason { get; private set; }

    public void MarkAsSuspicious(string? reason)
    {
        IsSuspicious = true;
        Reason = reason;
    }
}