using DashboardApplication.DTOs;

namespace DashboardApplication.Tests.DTOs;

public class PlayerActivityDtoTests
{
    [Fact]
    public void PlayerActivityDto_Constructor_ShouldSetProperties()
    {
        // Arrange
        var id = "1";
        var action = "Move";
        var timestamp = DateTime.UtcNow;
        var status = "Any status";
        var reason = "Repeated actions";

        // Act
        var playerActivityDto = new PlayerActivityDto(id, action, timestamp, status, reason);

        // Assert
        Assert.Equal(id, playerActivityDto.Id);
        Assert.Equal(action, playerActivityDto.Action);
        Assert.Equal(timestamp, playerActivityDto.Timestamp);
        Assert.Equal(status, playerActivityDto.Status);
        Assert.Equal(reason, playerActivityDto.Reason);
    }
}