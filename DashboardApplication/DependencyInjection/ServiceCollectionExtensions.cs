using DashboardApplication.Library.Detection;
using DashboardApplication.Library.Detection.Rules;
using DashboardApplication.Services;
using DashboardApplication.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardApplication.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddDashboardApplication(this IServiceCollection services)
    {
        // this can be extended to read settings from a configuration file
        // you could deploy instances of the api with detection or without detection
        services.AddScoped<IPlayerActivityService, PlayerActivityWithDetectionService>();
        services.AddScoped<IDetectionService, DetectionService>();
        services.AddScoped<IDetectionRule, IdenticalActionRule>();
        services.AddScoped<IDetectionRule, InhumanSpeedActionRule>();
    }
}