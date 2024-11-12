using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF;

public class BreweryDbContext : DbContext
{
    public DbSet<Domain.Entities.Brewery> Breweries { get; set; }
    public DbSet<Beer> Beers { get; set; }
    public DbSet<Brewer> Brewers { get; set; }

    public BreweryDbContext(DbContextOptions<BreweryDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.HasDefaultSchema("brewery");
    }
}