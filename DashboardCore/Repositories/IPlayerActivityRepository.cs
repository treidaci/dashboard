using DashboardCore.Entities;

namespace DashboardCore.Repositories;

public interface IPlayerActivityRepository
{
    Task<IEnumerable<PlayerActivity>> GetActivitiesByPlayerIdAsync(string playerId);
    Task AddPlayerActivityAsync(PlayerActivity playerActivity);
}