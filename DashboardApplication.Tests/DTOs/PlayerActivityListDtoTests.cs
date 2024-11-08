using DashboardApplication.DTOs;

namespace DashboardApplication.Tests.DTOs;

public class PlayerActivityListDtoTests
{
    [Fact]
    public void PlayerActivityListDto_Constructor_ShouldSetProperties()
    {
        // Arrange
        const string playerId = "Player123";
        var activities = new List<PlayerActivityDto>
        {
            new("1", "Move", DateTime.UtcNow, "Good", null),
            new("2", "Jump", DateTime.UtcNow, "Bad", "Inhuman speed")
        };

        // Act
        var playerActivityListDto = new PlayerActivityListDto(playerId, activities);

        // Assert
        Assert.Equal(playerId, playerActivityListDto.PlayerId);
        Assert.Equal(activities, playerActivityListDto.Activities);
    }
}