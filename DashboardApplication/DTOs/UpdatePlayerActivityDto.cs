namespace DashboardApplication.DTOs;

public class UpdatePlayerActivityDto(string id, string status, string? reason)
{
    public string Id { get; set; } = id;
    
    public string Status { get; set; } = status;
    
    public string? Reason { get; set; } = reason;
}