using DashboardApplication.DTOs;

namespace DashboardApplication.UseCases;

public interface IPlayerActivityService
{
    Task<PlayerActivityListDto> ListPlayerActivities(string playerId);
    Task CreatePlayerActivity(CreatePlayerActivityDto createPlayerActivityDto);
}