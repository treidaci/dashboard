using DashboardApplication.DTOs;
using DashboardApplication.Library.Detection;
using DashboardCore.Repositories;

namespace DashboardApplication.Services;

internal class PlayerActivityWithDetectionService(IPlayerActivityRepository repository, IDetectionService detectionService) : PlayerActivityService(repository)
{
    public override async Task<string> CreatePlayerActivity(string playerId, CreatePlayerActivityDto createPlayerActivityDto)
    {
        var id = await base.CreatePlayerActivity(playerId, createPlayerActivityDto);
        // fire & forget - this is happening in memory,
        // but it might as well be an async flow in which a message 
        // is sent and the detection service is triggered for analysis
        _ = detectionService.AnalysePlayerActivity(id, playerId);
        return id;
    }
}