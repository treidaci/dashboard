using DashboardCore.Repositories;
using DashboardDataAccess;
using DashboardDataAccess.DependencyInjection;
using DashboardDataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardDataAccessTests.DependencyInjection;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddDashboardDataAccess_RegistersDashboardDbContextWithSqlite()
    {
        // Arrange
        var services = new ServiceCollection();
        var connectionString = "DataSource=:memory:"; // Use an in-memory SQLite database for testing

        // Act
        services.AddDashboardDataAccess(connectionString);

        // Build the service provider to resolve services
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var dbContext = serviceProvider.GetService<DashboardDbContext>();
        Assert.NotNull(dbContext);
        Assert.IsType<DashboardDbContext>(dbContext);

        // Check that the database provider is SQLite
        var databaseProvider = dbContext.Database.ProviderName;
        Assert.Equal("Microsoft.EntityFrameworkCore.Sqlite", databaseProvider);
    }


    [Fact]
    public void AddDashboardDataAccess_RegistersPlayerActivityRepositoryAsScoped()
    {
        // Arrange
        var services = new ServiceCollection();
        var connectionString = "DataSource=:memory:";

        // Act
        services.AddDashboardDataAccess(connectionString);

        // Build the service provider to resolve services
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var repository = serviceProvider.GetService<IPlayerActivityRepository>();
        Assert.NotNull(repository);
        Assert.IsType<PlayerActivityRepository>(repository);

        // Verify lifetime as scoped
        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using var scope1 = scopeFactory.CreateScope();
        using var scope2 = scopeFactory.CreateScope();
        var repoInstance1 = scope1.ServiceProvider.GetService<IPlayerActivityRepository>();
        var repoInstance2 = scope2.ServiceProvider.GetService<IPlayerActivityRepository>();
        Assert.NotSame(repoInstance1, repoInstance2); // Scoped instances should differ across scopes
    }
}