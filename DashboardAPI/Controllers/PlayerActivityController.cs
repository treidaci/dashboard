using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace DashboardAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerActivityController(ILogger<PlayerActivityController> logger, IPlayerActivityService playerActivityService) : ControllerBase
{
    [HttpGet("activities/{playerId}")]
    public async Task<ActionResult<PlayerActivityListDto>> GetPlayerActivities(string playerId)
    {
        try
        {
            var playerActivities = await playerActivityService.ListPlayerActivities(playerId);
            
            if (playerActivities.Activities.Count == 0)
            {
                return NotFound($"No activities found for player with ID: {playerId}");
            }

            return Ok(playerActivities);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}