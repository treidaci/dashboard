using DashboardCore.Entities;
using DashboardCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DashboardDataAccess.Repositories
{
    public class PlayerActivityRepository(DashboardDbContext context) : IPlayerActivityRepository
    {
        public async Task<IEnumerable<PlayerActivity>> GetActivitiesByPlayerIdAsync(string playerId)
        {
            // Query the database for PlayerActivityDb entities with the matching PlayerId
            var activities = await context.PlayerActivities
                .Where(a => a.PlayerId == playerId)
                .ToListAsync();

            // Map each PlayerActivityDb to PlayerActivity - can use a mapper
            return activities.Select(a => new PlayerActivity(
                a.Id,
                a.PlayerId,
                a.Action,
                a.Timestamp,
                a.IsSuspicious,
                a.Reason
            )).ToList();
        }
    }
}