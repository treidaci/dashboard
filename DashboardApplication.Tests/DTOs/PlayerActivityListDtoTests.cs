using DashboardApplication.DTOs;

namespace DashboardApplication.Tests.DTOs;

public class PlayerActivityListDtoTests
{
    [Fact]
    public void PlayerActivityListDto_Constructor_ShouldSetProperties()
    {
        // Arrange
        var playerId = "Player123";
        var activities = new List<PlayerActivityDto>
        {
            new PlayerActivityDto("1", "Move", DateTime.UtcNow, false, null),
            new PlayerActivityDto("2", "Jump", DateTime.UtcNow, true, "Inhuman speed")
        };

        // Act
        var playerActivityListDto = new PlayerActivityListDto(playerId, activities);

        // Assert
        Assert.Equal(playerId, playerActivityListDto.PlayerId);
        Assert.Equal(activities, playerActivityListDto.Activities);
    }
}