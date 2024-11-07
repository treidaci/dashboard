using Microsoft.Extensions.DependencyInjection;

namespace DashboardDataAccess.DependencyInjection;

public static class ServiceScopeExtensions
{
    public static void EnsureSqliteCreated(this IServiceScope serviceScope)
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DashboardDbContext>();
        dbContext.Database.EnsureCreated();
    }
}