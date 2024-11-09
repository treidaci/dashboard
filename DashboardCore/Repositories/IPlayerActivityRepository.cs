using System.Linq.Expressions;
using DashboardCore.Entities;

namespace DashboardCore.Repositories;

public interface IPlayerActivityRepository
{
    Task<IEnumerable<PlayerActivity>> GetActivitiesByPlayerId(string playerId);
    Task AddActivity(PlayerActivity playerActivity);
    Task<PlayerActivity?> GetActivity(string id, string playerId);
    Task UpdateActivity(PlayerActivity activity);
    Task<IEnumerable<PlayerActivity>> GetActivitiesByFilter(Expression<Func<PlayerActivity, bool>> filter);
    Task<IEnumerable<PlayerActivity>> GetActivitiesWithinThreshold(PlayerActivity activity, TimeSpan threshold);
}