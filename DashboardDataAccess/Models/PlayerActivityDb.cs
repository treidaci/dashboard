namespace DashboardDataAccess.Models;

internal class PlayerActivityDb
{
    public string Id { get; set; }
    public string PlayerId { get; set; }
    public string Action { get; set; }
    public DateTime Timestamp { get; set; }
    
    public string Status { get; set; }
    public string? Reason { get; set; }
}