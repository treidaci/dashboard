using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using DashboardCore.Repositories;

namespace DashboardApplication.Services;

public class PlayerActivityService(IPlayerActivityRepository repository) : IPlayerActivityService
{
    public async Task<PlayerActivityListDto> ListPlayerActivities(string playerId)
    {
        var activities = await repository.GetActivitiesByPlayerIdAsync(playerId);

        // Map each PlayerActivity entity to a PlayerActivityDto - could use a mapper
        var activityDtos = activities.Select(activity => new PlayerActivityDto(
           activity.Id,
            activity.Action,
            activity.Timestamp,
            activity.IsSuspicious,
            activity.Reason
        )).ToList();

        return new PlayerActivityListDto(playerId, activityDtos);
    }
}