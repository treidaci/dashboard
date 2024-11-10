namespace DashboardDataAccess.Models;

public class PlayerStatusDb
{
    public string PlayerId { get; set; }
    
    public string Status { get; set; }
    
    public string? Reason { get; set; }
}