namespace DashboardCore.Entities;

public class PlayerStatus(string playerId, string status, string? reason)
{
    public string PlayerId { get; } = playerId;

    public string? Reason { get; private set; } = reason;
    
    public PlayerStatusType Status { get; private set; } =  Enum.Parse<PlayerStatusType>(status);
}