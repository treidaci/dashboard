namespace DashboardApplication.DTOs;

public class PlayerActivityDto(string id, string action, DateTime timestamp, string status, string? reason)
{
    public string Id { get; } = id;
    public string Action { get; } = action;
    public DateTime Timestamp { get; } = timestamp;
    public string Status { get; } = status;
    public string? Reason { get; } = reason;
}