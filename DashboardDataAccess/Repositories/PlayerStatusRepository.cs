using DashboardCore.Entities;
using DashboardCore.Repositories;
using DashboardDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardDataAccess.Repositories;

internal class PlayerStatusRepository(DashboardDbContext context) : IPlayerStatusRepository
{
    public async Task<PlayerStatus?> GetPlayerStatus(string playerId)
    {
        var playerStatusDb = await context.PlayerStatuses
            .FirstOrDefaultAsync(ps => ps.PlayerId == playerId);

        return playerStatusDb == null ? null : new PlayerStatus(
            playerStatusDb.PlayerId,
            playerStatusDb.Status,
            playerStatusDb.Reason
        );
    }

    public async Task CreatePlayerStatus(PlayerStatus playerStatus)
    {
        var playerStatusDb = new PlayerStatusDb
        {
            PlayerId = playerStatus.PlayerId,
            Status = playerStatus.Status.ToString(),
            Reason = playerStatus.Reason
        };

        await context.PlayerStatuses.AddAsync(playerStatusDb);
        await context.SaveChangesAsync();
    }

    public async Task UpdatePlayerStatus(PlayerStatus playerStatus)
    {
        var existingStatus = await context.PlayerStatuses
            .FirstOrDefaultAsync(ps => ps.PlayerId == playerStatus.PlayerId);

        if (existingStatus == null)
        {
            return;
        }

        existingStatus.Status = playerStatus.Status.ToString();
        existingStatus.Reason = playerStatus.Reason;

        await context.SaveChangesAsync();
    }
}