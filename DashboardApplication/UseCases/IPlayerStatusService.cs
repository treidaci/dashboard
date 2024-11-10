using DashboardApplication.DTOs;

namespace DashboardApplication.UseCases;

public interface IPlayerStatusService
{
    Task<PlayerStatusDto?> GetPlayerStatus(string playerId);
    Task<List<PlayerStatusListingDto>> GetPlayerStatuses();
    Task CreatePlayerStatus(string playerId, PlayerStatusDto playerStatusDto);
    Task UpdatePlayerStatus(string playerId, PlayerStatusDto playerStatusDto);
}