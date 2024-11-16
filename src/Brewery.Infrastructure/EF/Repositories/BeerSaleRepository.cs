using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class BeerSaleRepository : IBeerSaleRepository
{
    private readonly DbSet<BeerSale> _beerSales;
    private readonly BreweryDbContext _dbContext;

    public BeerSaleRepository(BreweryDbContext dbContext)
    {
        _beerSales = dbContext.BeerSales;
        _dbContext = dbContext;
    }

    public async Task AddAsync(BeerSale beerSale)
    {
        await _beerSales.AddAsync(beerSale);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(BeerSale beerSale)
    {
        _beerSales.Update(beerSale);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(BeerSale beerSale)
    {
        _beerSales.Remove(beerSale);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BeerSale>> BrowseByBeerId(Guid beerId)
        => await _beerSales.Where(b => b.BeerId == beerId).ToListAsync();
}