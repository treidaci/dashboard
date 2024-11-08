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
        // exception handling can be moved in a global exception filter or a middleware
        // so you don't have to duplicate try-catch blocks in all controller methods
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
    
    [HttpPost("activities")]
    public async Task<ActionResult> CreatePlayerActivity([FromBody] CreatePlayerActivityDto? createPlayerActivityDto)
    {
        if (createPlayerActivityDto == null)
        {
            return BadRequest("Invalid player activity data.");
        }

        await playerActivityService.CreatePlayerActivity(createPlayerActivityDto);

        // in this instance, returning no data from this endpoint
        // makes sense as creating player doesn't require any further input from the player
        return Ok();
    }
}