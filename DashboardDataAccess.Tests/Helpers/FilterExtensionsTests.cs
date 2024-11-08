using System.Linq.Expressions;
using DashboardDataAccess.Helpers;
using DashboardDataAccessTests.TestDoubles;

namespace DashboardDataAccessTests.Helpers;

public class FilterExtensionsTests
{
    [Fact]
    public void ExpressionConvert_ShouldConvertExpressionForMatchingProperties()
    {
        // Arrange
        Expression<Func<SourceType, bool>> sourceExpression = s => s.Name == "John" && s.Age > 30;

        // Act
        var convertedExpression = FilterExtensions.ExpressionConvert<DestinationType, SourceType>(sourceExpression);

        // Assert
        Assert.NotNull(convertedExpression);

        // Compile and test the expression
        var compiledExpression = convertedExpression.Compile();
        var testValue = new DestinationType { Name = "John", Age = 35 };
        var result = compiledExpression(testValue);

        Assert.True(result); // Should return true because the conditions match
    }

    [Fact]
    public void ExpressionConvert_ShouldReturnFalseForNonMatchingProperties()
    {
        // Arrange
        Expression<Func<SourceType, bool>> sourceExpression = s => s.Name == "Jane" && s.Age < 25;

        // Act
        var convertedExpression = FilterExtensions.ExpressionConvert<DestinationType, SourceType>(sourceExpression);

        // Assert
        Assert.NotNull(convertedExpression);
        
        // Compile and test the expression
        var compiledExpression = convertedExpression.Compile();
        var testValue = new DestinationType { Name = "John", Age = 35 };
        var result = compiledExpression(testValue);

        Assert.False(result); // Should return false because the conditions don't match
    }

    [Fact]
    public void ExpressionConvert_ShouldHandleSinglePropertyExpression()
    {
        // Arrange
        Expression<Func<SourceType, bool>> sourceExpression = s => s.Name == "Alice";

        // Act
        var convertedExpression = FilterExtensions.ExpressionConvert<DestinationType, SourceType>(sourceExpression);

        // Assert
        Assert.NotNull(convertedExpression);
        
        // Compile and test the expression
        var compiledExpression = convertedExpression.Compile();
        var testValue = new DestinationType { Name = "Alice", Age = 28 };
        var result = compiledExpression(testValue);

        Assert.True(result); // Should return true because Name matches
    }

    [Fact]
    public void ExpressionConvert_ShouldReturnFalseWhenMemberDoesNotExistInDestination()
    {
        // Arrange
        Expression<Func<SourceType, bool>> sourceExpression = s => s.Name == "NonExistent";

        // Act
        var convertedExpression = FilterExtensions.ExpressionConvert<DestinationType, SourceType>(sourceExpression);

        // Assert
        Assert.NotNull(convertedExpression);

        // Compile and test the expression
        var compiledExpression = convertedExpression.Compile();
        var testValue = new DestinationType { Name = "Other", Age = 25 };
        var result = compiledExpression(testValue);

        Assert.False(result); // Should return false because Name does not match
    }
}