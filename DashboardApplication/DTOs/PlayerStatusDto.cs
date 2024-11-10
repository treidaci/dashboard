namespace DashboardApplication.DTOs;

public class PlayerStatusDto(string status, string? reason)
{
    public string Status { get; } = status;
    public string? Reason { get; } = reason;
}