namespace DashboardApplication.DTOs;

public class PlayerActivityListDto(string playerId, List<PlayerActivityDto> activities)
{
    public string PlayerId { get; private set; } = playerId;
    public List<PlayerActivityDto> Activities { get; private set; } = activities;
}