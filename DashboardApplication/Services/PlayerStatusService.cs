using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using DashboardCore.Entities;
using DashboardCore.Repositories;

namespace DashboardApplication.Services;

public class PlayerStatusService(IPlayerStatusRepository repository) : IPlayerStatusService
{
    public async Task<PlayerStatusDto?> GetPlayerStatus(string playerId)
    {
        var playerStatus = await repository.GetPlayerStatus(playerId);
        return playerStatus == null ? null : new PlayerStatusDto(playerStatus.Status.ToString(), playerStatus.Reason);
    }

    public async Task CreatePlayerStatus(string playerId, PlayerStatusDto playerStatusDto)
    {
        var playerStatus = await repository.GetPlayerStatus(playerId);
        if (playerStatus != null)
        {
            // we could throw here if the player status already exists
            // chose to just return so we don't give away if the player status already exists or not
            return;
        }
        await repository.CreatePlayerStatus(new PlayerStatus(playerId, playerStatusDto.Status, playerStatusDto.Reason));
    }

    public async Task UpdatePlayerStatus(string playerId, PlayerStatusDto playerStatusDto)
    {
        var playerStatus = await repository.GetPlayerStatus(playerId);
        if (playerStatus == null)
        {
            return;
        }
        
        await repository.UpdatePlayerStatus(new PlayerStatus(playerId, playerStatusDto.Status, playerStatusDto.Reason));
    }
}