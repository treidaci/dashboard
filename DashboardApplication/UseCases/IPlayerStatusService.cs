using DashboardApplication.DTOs;

namespace DashboardApplication.UseCases;

public interface IPlayerStatusService
{
    Task<PlayerStatusDto?> GetPlayerStatus(string playerId);
    Task CreatePlayerStatus(string playerId, PlayerStatusDto playerStatusDto);
    Task UpdatePlayerStatus(string playerId, PlayerStatusDto playerStatusDto);
}