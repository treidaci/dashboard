using DashboardCore.Repositories;
using DashboardDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardDataAccess.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDashboardDataAccess(this IServiceCollection services, string connectionString)
    {
        // Register DbContext with SQLite
        services.AddDbContext<DashboardDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register repository as IPlayerActivityRepository
        services.AddScoped<IPlayerActivityRepository, PlayerActivityRepository>();

        return services;
    }
}