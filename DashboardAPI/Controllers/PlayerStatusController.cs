using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace DashboardAPI.Controllers;

[ApiController]
[Route("api/players")]
public class PlayerStatusController(IPlayerStatusService playerStatusService) : ControllerBase
{
    /// this endpoint could be an odata endpoint or a separate search service
    /// all together. I added here for brevity
    [HttpGet("statuses")]
    public async Task<ActionResult> GetPlayerStatuses()
    {
        var statuses = await playerStatusService.GetPlayerStatuses();
        if (statuses.Count == 0)
        {
            return NotFound($"No statuses found");
        }

        return Ok(statuses);
    }
    
    [HttpGet("{playerId}/status")]
    public async Task<ActionResult> GetPlayerStatus(string playerId)
    {
        var status = await playerStatusService.GetPlayerStatus(playerId);
        if (status == null)
        {
            return NotFound($"No status found for player with ID: {playerId}");
        }
        
        return Ok(status);
    }
    
    [HttpPost("{playerId}/status")]
    public async Task<ActionResult> CreatePlayerStatus(string playerId, [FromBody] PlayerStatusDto? playerStatusDto)
    {
        if (playerStatusDto == null)
        {
            return BadRequest("Invalid player status data.");
        }

        await playerStatusService.CreatePlayerStatus(playerId, playerStatusDto);
        
        return Ok();
    }
    
    [HttpPut("{playerId}/status")]
    public async Task<ActionResult> UpdatePlayerStatus(string playerId, [FromBody] PlayerStatusDto? playerStatusDto)
    {
        if (playerStatusDto == null)
        {
            return BadRequest("Invalid player status data.");
        }

        await playerStatusService.UpdatePlayerStatus(playerId, playerStatusDto);
        
        return Ok();
    }
}