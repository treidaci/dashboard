using DashboardApplication.DTOs;
using DashboardApplication.Library.Detection;
using DashboardApplication.Services;
using DashboardCore.Entities;
using DashboardCore.Repositories;
using Moq;

namespace DashboardApplication.Tests.Services;

public class PlayerActivityWithDetectionServiceTests
{
    private readonly Mock<IPlayerActivityRepository> _mockRepository;
    private readonly Mock<IDetectionService> _detectionServiceMock;
    private readonly PlayerActivityWithDetectionService _service;

    public PlayerActivityWithDetectionServiceTests()
    {
        _mockRepository = new Mock<IPlayerActivityRepository>();
        _detectionServiceMock = new Mock<IDetectionService>();
        _service = new PlayerActivityWithDetectionService(_mockRepository.Object, _detectionServiceMock.Object);
    }
    
    [Fact]
    public async Task CreatePlayerActivity_ShouldCallBaseCreatePlayerActivity()
    {
        // Arrange
        var playerId = "Player123";
        var createPlayerActivityDto = new CreatePlayerActivityDto
        {
            Action = "Move",
            Timestamp = DateTime.UtcNow
        };
        
        // Act
        var result = await _service.CreatePlayerActivity(playerId, createPlayerActivityDto);

        // Assert
        Assert.NotEmpty(result);

        _mockRepository.Verify(repo => repo.AddPlayerActivityAsync(It.IsAny<PlayerActivity>()), Times.Once,
            "AddPlayerActivityAsync should be called once to create the activity.");
    }

    [Fact]
    public async Task CreatePlayerActivity_ShouldTriggerDetectionServiceAnalysePlayerActivity()
    {
        // Arrange
        var playerId = "Player123";
        var createPlayerActivityDto = new CreatePlayerActivityDto
        {
            Action = "Move",
            Timestamp = DateTime.UtcNow
        };

        // Act
        var result = await _service.CreatePlayerActivity(playerId, createPlayerActivityDto);

        // Assert
        _detectionServiceMock.Verify(
            detectionService => detectionService.AnalysePlayerActivity(result, playerId),
            Times.Once,
            "AnalysePlayerActivity should be triggered once for the created activity."
        );
    }   
}