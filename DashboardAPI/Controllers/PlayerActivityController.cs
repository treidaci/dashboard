using Microsoft.AspNetCore.Mvc;

namespace DashboardAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerActivityController(ILogger<PlayerActivityController> logger) : ControllerBase
{

}