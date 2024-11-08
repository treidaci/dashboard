namespace DashboardApplication.DTOs;

public class CreatePlayerActivityDto
{
    public string PlayerId { get; set; }
    public string Action { get; set; }
    public DateTime Timestamp { get; set; }
}