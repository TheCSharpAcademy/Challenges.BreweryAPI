using Brewery.Abstractions.Postgres;
using Brewery.Infrastructure.EF;
using Brewery.Tests.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Tests.Shared.Fixtures;

public class BreweryDbFixture : IAsyncLifetime
{
    public BreweryDbContext BreweryDbContext { get; private set; }

    public BreweryDbFixture()
    {

    }
    
    public Task InitializeAsync()
    {
        var postgresOptions = OptionsHelper.GetOptions<PostgresOptions>("postgres");
        var dbContextOptions = new DbContextOptionsBuilder<BreweryDbContext>()
            .UseNpgsql(postgresOptions.ConnectionString)
            .Options;
        BreweryDbContext = new BreweryDbContext(dbContextOptions);
        
        BreweryDbContext.Database.EnsureDeleted();
        BreweryDbContext.Database.EnsureCreated();
        
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await BreweryDbContext.DisposeAsync();
    }

    public async Task SeedDatabaseAsync(Guid breweryId)
    {
        var brewery = Domain.Entities.Brewery.Create(breweryId, "Brewery 1");
        
        await BreweryDbContext.AddAsync(brewery);
        await BreweryDbContext.SaveChangesAsync();
    }
}