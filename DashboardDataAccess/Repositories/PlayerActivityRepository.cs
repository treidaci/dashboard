using DashboardCore.Entities;
using DashboardCore.Repositories;
using DashboardDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardDataAccess.Repositories
{
    internal class PlayerActivityRepository(DashboardDbContext context) : IPlayerActivityRepository
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
                Enum.Parse<PlayerActivityStatus>(a.Status),
                a.Reason
            )).ToList();
        }

        public async Task AddPlayerActivityAsync(PlayerActivity playerActivity)
        {
            await context.PlayerActivities.AddAsync(new PlayerActivityDb
            {
                Id = playerActivity.Id,
                PlayerId = playerActivity.PlayerId, 
                Action = playerActivity.Action, 
                Timestamp = playerActivity.Timestamp, 
                Status = playerActivity.Status.ToString(), 
                Reason = playerActivity.Reason
            });
            
            // Save changes to the database
            await context.SaveChangesAsync();
        }
    }
}