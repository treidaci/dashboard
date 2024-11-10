using DashboardDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardDataAccess;

internal class DashboardDbContext(DbContextOptions<DashboardDbContext> options) : DbContext(options)
{
    public DbSet<PlayerActivityDb> PlayerActivities { get; set; }
    public DbSet<PlayerStatusDb> PlayerStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerActivityDb>().ToTable("PlayerActivities");
        modelBuilder.Entity<PlayerActivityDb>().HasKey(a => a.Id);
        
        modelBuilder.Entity<PlayerStatusDb>().ToTable("PlayerStatuses");
        modelBuilder.Entity<PlayerStatusDb>().HasKey(a => a.PlayerId);
    }
}