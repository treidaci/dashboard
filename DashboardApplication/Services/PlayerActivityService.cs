using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using DashboardCore.Entities;
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

    public async Task CreatePlayerActivity(CreatePlayerActivityDto createPlayerActivityDto)
    {
        var playerActivity = new PlayerActivity(
            id: Guid.NewGuid().ToString(), // create id here maybe we want to use it further down the line
            playerId: createPlayerActivityDto.PlayerId,
            action: createPlayerActivityDto.Action,
            timestamp: createPlayerActivityDto.Timestamp
        );
        
        await repository.AddPlayerActivityAsync(playerActivity);
    }
}