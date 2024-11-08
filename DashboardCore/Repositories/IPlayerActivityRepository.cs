using System.Linq.Expressions;
using DashboardCore.Entities;

namespace DashboardCore.Repositories;

public interface IPlayerActivityRepository
{
    Task<IEnumerable<PlayerActivity>> GetActivitiesByPlayerIdAsync(string playerId);
    Task AddPlayerActivityAsync(PlayerActivity playerActivity);
    Task<PlayerActivity?> GetActivity(string id, string playerId);
    Task UpdateActivity(PlayerActivity activity);
    Task<IEnumerable<PlayerActivity>> GetActivitiesByFilter(Expression<Func<PlayerActivity, bool>> filter);
}