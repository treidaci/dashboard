using DashboardApplication.DependencyInjection;
using DashboardApplication.Library.Detection;
using DashboardApplication.Library.Detection.Rules;
using DashboardApplication.Services;
using DashboardApplication.UseCases;
using DashboardCore.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace DashboardApplication.Tests.DependencyInjection;

public class ServiceCollectionExtensionsTests
{
    private readonly ServiceProvider _serviceProvider;

    public ServiceCollectionExtensionsTests()
    {
        var services = new ServiceCollection();

        // Register application services
        services.AddDashboardApplication();

        // Register mocked dependencies
        services.AddScoped(_ => new Mock<IPlayerActivityRepository>().Object);
        services.AddScoped(_ => new Mock<IPlayerStatusRepository>().Object);

        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void AddDashboardApplication_ShouldRegisterPlayerActivityServiceWithDetection()
    {
        // Act
        var playerActivityService = _serviceProvider.GetService<IPlayerActivityService>();

        // Assert
        Assert.NotNull(playerActivityService);
        Assert.IsType<PlayerActivityWithDetectionService>(playerActivityService);
    }

    [Fact]
    public void AddDashboardApplication_ShouldRegisterDetectionService()
    {
        // Act
        var detectionService = _serviceProvider.GetService<IDetectionService>();

        // Assert
        Assert.NotNull(detectionService);
        Assert.IsType<DetectionService>(detectionService);
    }

    [Fact]
    public void AddDashboardApplication_ShouldRegisterDetectionRules()
    {
        // Act
        var allRules = _serviceProvider.GetServices<IDetectionRule>().ToList();

        // Assert
        Assert.NotEmpty(allRules);
        Assert.Contains(allRules, rule => rule is IdenticalActionRule);
        Assert.Contains(allRules, rule => rule is InhumanSpeedActionRule);
    }

    [Fact]
    public void AddDashboardApplication_ShouldRegisterPlayerStatusService()
    {
        // Act
        var playerStatusService = _serviceProvider.GetService<IPlayerStatusService>();

        // Assert
        Assert.NotNull(playerStatusService);
        Assert.IsType<PlayerStatusService>(playerStatusService);
    }

    [Fact]
    public void AddDashboardApplication_ShouldRegisterServicesAsScoped()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDashboardApplication();
        services.AddScoped(_ => new Mock<IPlayerActivityRepository>().Object);
        services.AddScoped(_ => new Mock<IPlayerStatusRepository>().Object);
        
        services.BuildServiceProvider();

        // Assert Scoped Lifetimes
        Assert.Equal(ServiceLifetime.Scoped, GetServiceLifetime<IPlayerActivityService>(services));
        Assert.Equal(ServiceLifetime.Scoped, GetServiceLifetime<IDetectionService>(services));
        Assert.Equal(ServiceLifetime.Scoped, GetServiceLifetime<IDetectionRule>(services));
        Assert.Equal(ServiceLifetime.Scoped, GetServiceLifetime<IPlayerStatusService>(services));
    }

    private ServiceLifetime? GetServiceLifetime<T>(IServiceCollection services)
    {
        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T));
        return descriptor?.Lifetime;
    }
}