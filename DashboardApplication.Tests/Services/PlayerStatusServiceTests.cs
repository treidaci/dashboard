using DashboardApplication.DTOs;
using DashboardApplication.Services;
using DashboardCore.Entities;
using DashboardCore.Repositories;
using Moq;

namespace DashboardApplication.Tests.Services;

public class PlayerStatusServiceTests
{
    private readonly Mock<IPlayerStatusRepository> _mockRepository;
    private readonly PlayerStatusService _service;

    public PlayerStatusServiceTests()
    {
        _mockRepository = new Mock<IPlayerStatusRepository>();
        _service = new PlayerStatusService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetPlayerStatus_ShouldReturnPlayerStatusDto_WhenStatusExists()
    {
        // Arrange
        var playerId = "Player123";
        var playerStatus = new PlayerStatus(playerId, "Active", "Player is active");

        _mockRepository.Setup(repo => repo.GetPlayerStatus(playerId))
            .ReturnsAsync(playerStatus);

        // Act
        var result = await _service.GetPlayerStatus(playerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Active", result.Status);
        Assert.Equal("Player is active", result.Reason);
    }

    [Fact]
    public async Task GetPlayerStatus_ShouldReturnNull_WhenStatusDoesNotExist()
    {
        // Arrange
        var playerId = "Player123";

        _mockRepository.Setup(repo => repo.GetPlayerStatus(playerId))
            .ReturnsAsync((PlayerStatus?)null);

        // Act
        var result = await _service.GetPlayerStatus(playerId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreatePlayerStatus_ShouldCreateNewStatus_WhenStatusDoesNotExist()
    {
        // Arrange
        var playerId = "Player123";
        var playerStatusDto = new PlayerStatusDto("Active", "Player is active");

        _mockRepository.Setup(repo => repo.GetPlayerStatus(playerId))
            .ReturnsAsync((PlayerStatus?)null);

        // Act
        await _service.CreatePlayerStatus(playerId, playerStatusDto);

        // Assert
        _mockRepository.Verify(
            repo => repo.CreatePlayerStatus(It.Is<PlayerStatus>(
                status => status.PlayerId == playerId &&
                          status.Status == PlayerStatusType.Active &&
                          status.Reason == "Player is active"
            )),
            Times.Once,
            "CreatePlayerStatus should be called once with the provided status"
        );
    }

    [Fact]
    public async Task CreatePlayerStatus_ShouldNotCreateStatus_WhenStatusAlreadyExists()
    {
        // Arrange
        var playerId = "Player123";
        var playerStatusDto = new PlayerStatusDto("Active", "Player is active");
        var existingStatus = new PlayerStatus(playerId, "Active", "Player is active");

        _mockRepository.Setup(repo => repo.GetPlayerStatus(playerId))
            .ReturnsAsync(existingStatus);

        // Act
        await _service.CreatePlayerStatus(playerId, playerStatusDto);

        // Assert
        _mockRepository.Verify(repo => repo.CreatePlayerStatus(It.IsAny<PlayerStatus>()), Times.Never,
            "CreatePlayerStatus should not be called if the status already exists");
    }

    [Fact]
    public async Task UpdatePlayerStatus_ShouldUpdateStatus_WhenStatusExists()
    {
        // Arrange
        var playerId = "Player123";
        var existingStatus = new PlayerStatus(playerId, "Active", "Player is active");
        var playerStatusDto = new PlayerStatusDto("Suspicious", "Suspicious activity detected");

        _mockRepository.Setup(repo => repo.GetPlayerStatus(playerId))
            .ReturnsAsync(existingStatus);

        // Act
        await _service.UpdatePlayerStatus(playerId, playerStatusDto);

        // Assert
        _mockRepository.Verify(
            repo => repo.UpdatePlayerStatus(It.Is<PlayerStatus>(
                status => status.PlayerId == playerId &&
                          status.Status == PlayerStatusType.Suspicious &&
                          status.Reason == "Suspicious activity detected"
            )),
            Times.Once,
            "UpdatePlayerStatus should be called once with the updated status"
        );
    }

    [Fact]
    public async Task UpdatePlayerStatus_ShouldNotUpdateStatus_WhenStatusDoesNotExist()
    {
        // Arrange
        var playerId = "Player123";
        var playerStatusDto = new PlayerStatusDto("Suspicious", "Suspicious activity detected");

        _mockRepository.Setup(repo => repo.GetPlayerStatus(playerId))
            .ReturnsAsync((PlayerStatus?)null);

        // Act
        await _service.UpdatePlayerStatus(playerId, playerStatusDto);

        // Assert
        _mockRepository.Verify(repo => repo.UpdatePlayerStatus(It.IsAny<PlayerStatus>()), Times.Never,
            "UpdatePlayerStatus should not be called if the status does not exist");
    }
}