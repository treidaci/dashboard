using System.Linq.Expressions;
using DashboardCore.Entities;
using DashboardCore.Repositories;
using DashboardDataAccess.Helpers;
using DashboardDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardDataAccess.Repositories;

internal class PlayerActivityRepository(DashboardDbContext context) : IPlayerActivityRepository
{
    public async Task<IEnumerable<PlayerActivity>> GetActivitiesByPlayerId(string playerId)
    {
        var activities = await context.PlayerActivities
            .Where(a => a.PlayerId == playerId)
            .ToListAsync();

        return PlayerActivitiesDbToPlayerActivities(activities);
    }

    public async Task AddActivity(PlayerActivity playerActivity)
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
        
        await context.SaveChangesAsync();
    }

    public async Task<PlayerActivity?> GetActivity(string id, string playerId)
    {
        var activityDb = await context.PlayerActivities
            .Where(a => a.Id==id &&  a.PlayerId == playerId)
            .FirstOrDefaultAsync();
        if (activityDb == null) return null;
            
        return new PlayerActivity(activityDb.Id, activityDb.PlayerId, activityDb.Action, activityDb.Timestamp, 
            activityDb.Status, activityDb.Reason);
    }

    public async Task UpdateActivity(PlayerActivity activity)
    {
        var existingActivity = await context.PlayerActivities
            .FirstOrDefaultAsync(a => a.Id == activity.Id && a.PlayerId == activity.PlayerId);

        if (existingActivity == null)
        {
            return;
        }
        
        existingActivity.Action = activity.Action;
        existingActivity.Timestamp = activity.Timestamp;
        existingActivity.Status = activity.Status.ToString();
        existingActivity.Reason = activity.Reason;
        
        await context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<PlayerActivity>> GetActivitiesByFilter(Expression<Func<PlayerActivity, bool>> filter)
    {
        // Convert PlayerActivity filter expression to PlayerActivityDb expression
        var dbFilter = FilterExtensions.ExpressionConvert<PlayerActivityDb, PlayerActivity>(filter);

        var activities = await context.PlayerActivities
            .Where(dbFilter)
            .ToListAsync();

        return PlayerActivitiesDbToPlayerActivities(activities);
    }

    public async Task<IEnumerable<PlayerActivity>> GetActivitiesWithinThreshold(PlayerActivity activity, TimeSpan threshold)
    {
        // Define the time range based on the threshold
        var startTime = activity.Timestamp - threshold;
        var endTime = activity.Timestamp + threshold;
        
        var activities = await context.PlayerActivities
            .Where(a=>a.PlayerId == activity.PlayerId && 
                      a.Timestamp >= startTime &&
                      a.Timestamp <= endTime &&
                      a.Id != activity.Id)
            .ToListAsync();

        return PlayerActivitiesDbToPlayerActivities(activities);
    }

    private static IEnumerable<PlayerActivity> PlayerActivitiesDbToPlayerActivities(List<PlayerActivityDb> activities)
    {
        // Map each PlayerActivityDb to PlayerActivity - could use a mapper
        return activities.Select(a => new PlayerActivity(
            a.Id,
            a.PlayerId,
            a.Action,
            a.Timestamp,
            a.Status,
            a.Reason
        )).ToList();
    }
}