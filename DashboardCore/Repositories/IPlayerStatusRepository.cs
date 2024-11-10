using DashboardCore.Entities;

namespace DashboardCore.Repositories;

public interface IPlayerStatusRepository
{
    Task<PlayerStatus?> GetPlayerStatus(string playerId);
    Task CreatePlayerStatus(PlayerStatus playerStatus);
    Task UpdatePlayerStatus(PlayerStatus playerStatus);
}