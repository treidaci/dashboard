using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace DashboardAPI.Controllers;

[ApiController]
[Route("api/players/{playerId}/status")]
public class PlayerStatusController(IPlayerStatusService playerStatusService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetPlayerStatus(string playerId)
    {

        var status = await playerStatusService.GetPlayerStatus(playerId);
        if (status == null)
        {
            return NotFound($"No status found for player with ID: {playerId}");
        }
        
        return Ok(status);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreatePlayerStatus(string playerId, [FromBody] PlayerStatusDto? playerStatusDto)
    {
        if (playerStatusDto == null)
        {
            return BadRequest("Invalid player status data.");
        }

        await playerStatusService.CreatePlayerStatus(playerId, playerStatusDto);
        
        return Ok();
    }
    
    [HttpPut]
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