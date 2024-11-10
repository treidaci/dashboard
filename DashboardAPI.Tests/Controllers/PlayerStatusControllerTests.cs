using DashboardAPI.Controllers;
using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DashboardAPI.Tests.Controllers;

public class PlayerStatusControllerTests
{
    private readonly Mock<IPlayerStatusService> _mockPlayerStatusService;
    private readonly PlayerStatusController _controller;

    public PlayerStatusControllerTests()
    {
        _mockPlayerStatusService = new Mock<IPlayerStatusService>();
        _controller = new PlayerStatusController(_mockPlayerStatusService.Object);
    }

    [Fact]
    public async Task GetPlayerStatus_ShouldReturnOk_WhenStatusIsFound()
    {
        // Arrange
        var playerId = "Player123";
        var playerStatus = new PlayerStatusDto("Active", "Player is active");

        _mockPlayerStatusService
            .Setup(service => service.GetPlayerStatus(playerId))
            .ReturnsAsync(playerStatus);

        // Act
        var result = await _controller.GetPlayerStatus(playerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(playerStatus, okResult.Value);
    }

    [Fact]
    public async Task GetPlayerStatus_ShouldReturnNotFound_WhenStatusIsNotFound()
    {
        // Arrange
        var playerId = "Player123";

        _mockPlayerStatusService
            .Setup(service => service.GetPlayerStatus(playerId))
            .ReturnsAsync((PlayerStatusDto?)null);

        // Act
        var result = await _controller.GetPlayerStatus(playerId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.Equal($"No status found for player with ID: {playerId}", notFoundResult.Value);
    }

    [Fact]
    public async Task CreatePlayerStatus_ShouldReturnBadRequest_WhenDtoIsNull()
    {
        // Arrange
        var playerId = "Player123";

        // Act
        var result = await _controller.CreatePlayerStatus(playerId, null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Invalid player status data.", badRequestResult.Value);
    }

    [Fact]
    public async Task CreatePlayerStatus_ShouldReturnOk_WhenDtoIsValid()
    {
        // Arrange
        var playerId = "Player123";
        var playerStatusDto = new PlayerStatusDto("Active", "Player is active");

        // Act
        var result = await _controller.CreatePlayerStatus(playerId, playerStatusDto);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        _mockPlayerStatusService.Verify(
            service => service.CreatePlayerStatus(playerId, playerStatusDto),
            Times.Once,
            "CreatePlayerStatus should be called once with the provided DTO"
        );
    }

    [Fact]
    public async Task UpdatePlayerStatus_ShouldReturnBadRequest_WhenDtoIsNull()
    {
        // Arrange
        var playerId = "Player123";

        // Act
        var result = await _controller.UpdatePlayerStatus(playerId, null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Invalid player status data.", badRequestResult.Value);
    }

    [Fact]
    public async Task UpdatePlayerStatus_ShouldReturnOk_WhenDtoIsValid()
    {
        // Arrange
        var playerId = "Player123";
        var playerStatusDto = new PlayerStatusDto("Inactive", "Player has been inactive");

        // Act
        var result = await _controller.UpdatePlayerStatus(playerId, playerStatusDto);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        _mockPlayerStatusService.Verify(
            service => service.UpdatePlayerStatus(playerId, playerStatusDto),
            Times.Once,
            "UpdatePlayerStatus should be called once with the provided DTO"
        );
    }
    
    [Fact]
    public async Task GetPlayerStatuses_ShouldReturnOk_WhenStatusesAreFound()
    {
        // Arrange
        var statuses = new List<PlayerStatusListingDto>
        {
            new PlayerStatusListingDto("Player123", "Active", "Player is active"),
            new PlayerStatusListingDto("Player456", "Suspended", "Suspicious activity detected")
        };

        _mockPlayerStatusService
            .Setup(service => service.GetPlayerStatuses())
            .ReturnsAsync(statuses);

        // Act
        var result = await _controller.GetPlayerStatuses();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(statuses, okResult.Value);
    }

    [Fact]
    public async Task GetPlayerStatuses_ShouldReturnNotFound_WhenNoStatusesAreFound()
    {
        // Arrange
        _mockPlayerStatusService
            .Setup(service => service.GetPlayerStatuses())
            .ReturnsAsync(new List<PlayerStatusListingDto>()); // Empty list

        // Act
        var result = await _controller.GetPlayerStatuses();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.Equal("No statuses found", notFoundResult.Value);
    }
}