using DashboardApplication.DTOs;

namespace DashboardApplication.UseCases;

public interface IPlayerActivityService
{
    Task<PlayerActivityListDto> ListPlayerActivities(string playerId);
    Task<string> CreatePlayerActivity(string playerId, CreatePlayerActivityDto createPlayerActivityDto);
    Task UpdatePlayerActivity(string playerId, UpdatePlayerActivityDto updatePlayerActivityDto);
}