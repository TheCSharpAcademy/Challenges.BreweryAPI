using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Brewery.Infrastructure.Services;

public class AppInitializer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AppInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => typeof(DbContext).IsAssignableFrom(t) && !t.IsInterface && t != typeof(DbContext));
        using var scope = _serviceProvider.CreateScope();
        foreach (var dbType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetService(dbType) as DbContext;
            await dbContext.Database.MigrateAsync();
        }
    }

    private async Task SeedDatabase()
    {
        
    }
}