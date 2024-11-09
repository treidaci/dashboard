using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace DashboardAPI.Controllers;

[ApiController]
[Route("api/players/{playerId}")]
public class PlayerActivityController(ILogger<PlayerActivityController> logger, IPlayerActivityService playerActivityService) : ControllerBase
{
    [HttpGet("activities")]
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
    
    [HttpPost("activity")]
    public async Task<ActionResult> CreatePlayerActivity(string playerId, [FromBody] CreatePlayerActivityDto? createPlayerActivityDto)
    {
        if (createPlayerActivityDto == null)
        {
            return BadRequest("Invalid player activity data.");
        }

        await playerActivityService.CreatePlayerActivity(playerId, createPlayerActivityDto);

        // in this instance, returning no data from this endpoint
        // makes sense as creating player doesn't require any further input from the player
        return Ok();
    }
    
    [HttpPut("activity")]
    public async Task<ActionResult> UpdatePlayerActivity(string playerId, [FromBody] UpdatePlayerActivityDto? updatePlayerActivityDto)
    {
        if (updatePlayerActivityDto == null)
        {
            return BadRequest("Invalid player activity data.");
        }

        await playerActivityService.UpdatePlayerActivity(playerId, updatePlayerActivityDto);
        
        return Ok();
    }
}