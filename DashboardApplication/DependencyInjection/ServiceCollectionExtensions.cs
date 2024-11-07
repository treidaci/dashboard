using DashboardApplication.Services;
using DashboardApplication.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardApplication.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDashboardApplication(this IServiceCollection services)
    {
        services.AddScoped<IPlayerActivityService, PlayerActivityService>();

        return services;
    }
}