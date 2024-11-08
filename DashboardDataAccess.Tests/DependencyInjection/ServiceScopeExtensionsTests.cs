using DashboardDataAccess;
using DashboardDataAccess.DependencyInjection;
using DashboardDataAccessTests.TestDoubles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace DashboardDataAccessTests.DependencyInjection;

public class ServiceScopeExtensionsTests
{
    [Fact]
    public void EnsureSqliteCreated_ShouldCallEnsureCreatedOnDbContext()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DashboardDbContext>()
            .UseInMemoryDatabase("TestDatabase") // Using in-memory database for testing
            .Options;

        var testDbContext = new TestDbContext(options);

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(DashboardDbContext)))
            .Returns(testDbContext);

        var serviceScopeMock = new Mock<IServiceScope>();
        serviceScopeMock
            .Setup(scope => scope.ServiceProvider)
            .Returns(serviceProviderMock.Object);

        // Act
        serviceScopeMock.Object.EnsureSqliteCreated();

        // Assert
        testDbContext.MockDatabase.Verify(db => db.EnsureCreated(), Times.Once, "EnsureCreated was not called on DashboardDbContext.");
    }
}