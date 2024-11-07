namespace DashboardApplication.DTOs;

public class PlayerActivityDto(string id, string action, DateTime timestamp, bool isSuspicious, string? reason)
{
    public string Id { get; set; } = id;
    public string Action { get; set; } = action;
    public DateTime Timestamp { get; set; } = timestamp;
    public bool IsSuspicious { get; set; } = isSuspicious;
    public string? Reason { get; set; } = reason;
}