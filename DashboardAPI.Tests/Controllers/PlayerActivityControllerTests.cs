using DashboardAPI.Controllers;
using DashboardApplication.DTOs;
using DashboardApplication.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DashboardAPI.Tests.Controllers;

public class PlayerActivityControllerTests
{
    private readonly Mock<IPlayerActivityService> _mockPlayerActivityService;
    private readonly PlayerActivityController _controller;

    public PlayerActivityControllerTests()
    {
        _mockPlayerActivityService = new Mock<IPlayerActivityService>();
        Mock<ILogger<PlayerActivityController>> mockLogger = new();
        _controller = new PlayerActivityController(mockLogger.Object, _mockPlayerActivityService.Object);
    }

    [Fact]
    public async Task GetPlayerActivities_ShouldReturnOk_WhenActivitiesAreFound()
    {
        // Arrange
        var playerId = "Player123";
        var playerActivities = new PlayerActivityListDto(playerId, [
            new PlayerActivityDto("1", "Move", DateTime.UtcNow, false, null),
            new PlayerActivityDto("2", "Jump", DateTime.UtcNow, true, "Inhuman speed")
        ]);

        _mockPlayerActivityService
            .Setup(service => service.ListPlayerActivities(playerId))
            .ReturnsAsync(playerActivities);

        // Act
        var result = await _controller.GetPlayerActivities(playerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        var returnValue = Assert.IsType<PlayerActivityListDto>(okResult.Value);
        Assert.Equal(playerId, returnValue.PlayerId);
        Assert.Equal(2, returnValue.Activities.Count);
    }

    [Fact]
    public async Task GetPlayerActivities_ShouldReturnNotFound_WhenNoActivitiesAreFound()
    {
        // Arrange
        var playerId = "Player123";
        var playerActivities = new PlayerActivityListDto(playerId, new List<PlayerActivityDto>());

        _mockPlayerActivityService
            .Setup(service => service.ListPlayerActivities(playerId))
            .ReturnsAsync(playerActivities);

        // Act
        var result = await _controller.GetPlayerActivities(playerId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.Equal($"No activities found for player with ID: {playerId}", notFoundResult.Value);
    }

    [Fact]
    public async Task GetPlayerActivities_ShouldReturnServerError_WhenExceptionIsThrown()
    {
        // Arrange
        var playerId = "Player123";

        _mockPlayerActivityService
            .Setup(service => service.ListPlayerActivities(playerId))
            .ThrowsAsync(new Exception("An error occurred"));

        // Act
        var result = await _controller.GetPlayerActivities(playerId);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("An error occurred while processing your request.", objectResult.Value);
    }
    
    [Fact]
    public async Task CreatePlayerActivity_ShouldReturnOk_WhenActivityIsCreated()
    {
        // Arrange
        var playerId = "Player123";
        var createPlayerActivityDto = new CreatePlayerActivityDto
        {
            Action = "Move",
            Timestamp = DateTime.UtcNow
        };

        // Act
        var result = await _controller.CreatePlayerActivity(playerId, createPlayerActivityDto);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        _mockPlayerActivityService.Verify(
            service => service.CreatePlayerActivity(playerId, createPlayerActivityDto),
            Times.Once
        );
    }

    [Fact]
    public async Task CreatePlayerActivity_ShouldReturnBadRequest_WhenDtoIsNull()
    {
        // Arrange
        var playerId = "Player123";
        // Act
        var result = await _controller.CreatePlayerActivity(playerId, null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Invalid player activity data.", badRequestResult.Value);

        _mockPlayerActivityService.Verify(
            service => service.CreatePlayerActivity(playerId, It.IsAny<CreatePlayerActivityDto>()),
            Times.Never
        );
    }
}