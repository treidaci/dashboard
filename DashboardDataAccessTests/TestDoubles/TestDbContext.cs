using DashboardDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;

namespace DashboardDataAccessTests.TestDoubles;

internal class TestDbContext : DashboardDbContext
{
    private readonly Mock<DatabaseFacade> _mockDatabaseFacade;

    public TestDbContext(DbContextOptions<DashboardDbContext> options) : base(options)
    {
        _mockDatabaseFacade = new Mock<DatabaseFacade>(this);
        _mockDatabaseFacade.Setup(db => db.EnsureCreated()).Verifiable();
    }

    public override DatabaseFacade Database => _mockDatabaseFacade.Object;

    public Mock<DatabaseFacade> MockDatabase => _mockDatabaseFacade;
}