using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using DashboardCore.Entities;
using DashboardCore.Repositories;

namespace DashboardApplication.Services;

internal class PlayerActivityService(IPlayerActivityRepository repository) : IPlayerActivityService
{
    public virtual async Task<PlayerActivityListDto> ListPlayerActivities(string playerId)
    {
        var activities = await repository.GetActivitiesByPlayerIdAsync(playerId);

        // Map each PlayerActivity entity to a PlayerActivityDto - could use a mapper
        var activityDtos = activities.Select(activity => new PlayerActivityDto(
           activity.Id,
            activity.Action,
            activity.Timestamp,
            activity.Status.ToString(),
            activity.Reason
        )).ToList();

        return new PlayerActivityListDto(playerId, activityDtos);
    }

    public virtual async Task<string> CreatePlayerActivity(string playerId, CreatePlayerActivityDto createPlayerActivityDto)
    {
        var playerActivity = new PlayerActivity(
            id: Guid.NewGuid().ToString(), // create id here maybe we want to use it further down the line
            playerId,
            action: createPlayerActivityDto.Action,
            timestamp: createPlayerActivityDto.Timestamp
        );
        
        await repository.AddPlayerActivityAsync(playerActivity);
        
        return playerActivity.Id;
    }
}