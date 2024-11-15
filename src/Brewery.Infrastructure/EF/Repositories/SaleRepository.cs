using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DbSet<BeerSale> _sales;
    private readonly BreweryDbContext _dbContext;

    public SaleRepository(BreweryDbContext dbContext)
    {
        _sales = _dbContext.BeerSales;
        _dbContext = dbContext;
    }

    public async Task AddAsync(BeerSale beerSale)
    {
        await _sales.AddAsync(beerSale);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(BeerSale beerSale)
    {
        _sales.Update(beerSale);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(BeerSale beerSale)
    {
        _sales.Remove(beerSale);
        await _dbContext.SaveChangesAsync();
    }

    public Task<BeerSale> GetSaleByBeerId(Guid beerId)
        => _sales.SingleOrDefaultAsync(s => s.BeerId == beerId);
}