using DashboardDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardDataAccess;

internal class DashboardDbContext(DbContextOptions<DashboardDbContext> options) : DbContext(options)
{
    public DbSet<PlayerActivityDb> PlayerActivities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerActivityDb>().ToTable("PlayerActivities");
        modelBuilder.Entity<PlayerActivityDb>().HasKey(a => a.Id);
    }
}