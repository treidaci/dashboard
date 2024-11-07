using DashboardApplication.DependencyInjection;
using DashboardDataAccess.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add layers
builder.Services.AddDashboardDataAccess(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);
builder.Services.AddDashboardApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    // Ensure Sqlite database is created
    scope.EnsureSqliteCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();