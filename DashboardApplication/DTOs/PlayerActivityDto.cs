namespace DashboardApplication.DTOs;

public class PlayerActivityDto(string id, string action, DateTime timestamp, string status, string? reason)
{
    public string Id { get; set; } = id;
    public string Action { get; set; } = action;
    public DateTime Timestamp { get; set; } = timestamp;
    public string Status { get; set; } = status;
    public string? Reason { get; set; } = reason;
}