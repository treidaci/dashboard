using DashboardCore.Repositories;
using DashboardDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardDataAccess.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddDashboardDataAccess(this IServiceCollection services, string connectionString)
    {
        // Register DbContext with SQLite
        services.AddDbContext<DashboardDbContext>(options =>
            options.UseSqlite(connectionString));
        
        // Register repositories
        services.AddScoped<IPlayerActivityRepository, PlayerActivityRepository>();
        services.AddScoped<IPlayerStatusRepository, PlayerStatusRepository>();
    }
}