namespace DashboardApplication.DTOs;

public class PlayerStatusListingDto(string playerId, string status, string? reason) : PlayerStatusDto(status, reason)
{
    public string PlayerId { get; private set; } = playerId;
}