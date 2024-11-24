using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class BeerStockRepository : IBeerStockRepository
{
    private readonly DbSet<BeerStock> _beerStocks;
    private readonly BreweryDbContext _dbContext;

    public BeerStockRepository(BreweryDbContext dbContext)
    {
        _beerStocks = dbContext.BeerStocks;
        _dbContext = dbContext;
    }

    public async Task AddBeerStock(BeerStock stock)
    {
        await _beerStocks.AddAsync(stock);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBeerStock(BeerStock stock)
    {
        _beerStocks.Update(stock);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBeerStock(BeerStock stock)
    {
        _beerStocks.Remove(stock);
        await _dbContext.SaveChangesAsync();
    }

    public Task<BeerStock> GetBeerStock(Guid beerId)
        => _beerStocks.SingleOrDefaultAsync(b => b.BeerId == beerId);
}