using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF;

public class BreweryDbContext : DbContext
{

    public DbSet<Beer> Beers { get; set; }
    public DbSet<Brewer> Brewers { get; set; }
    public DbSet<Domain.Entities.Brewery> Breweries { get; set; }
    public DbSet<Wholesaler> Wholesalers { get; set; }
    public DbSet<BeerSale> BeerSales { get; set; }
    public DbSet<BeerStock> BeerStocks { get; set; }
    public DbSet<BeerOrder> BeerOrder { get; set; }
    public DbSet<BeerQuote> BeerQuotes { get; set; }
    public DbSet<User> Users { get; set; }

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