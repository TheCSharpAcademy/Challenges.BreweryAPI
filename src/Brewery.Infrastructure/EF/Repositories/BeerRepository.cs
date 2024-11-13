using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class BeerRepository : IBeerRepository
{
    private readonly DbSet<Beer> _beers;
    private readonly BreweryDbContext _dbContext;

    public BeerRepository(BreweryDbContext dbContext)
    {
        _beers = dbContext.Beers;
        _dbContext = dbContext;
    }

    public async Task AddAsync(Beer beer)
    {
        await _beers.AddAsync(beer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Beer beer)
    {
        _beers.Update(beer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Beer beer)
    {
        _beers.Remove(beer);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Beer> GetBeerById(Guid id)
        => _beers.SingleOrDefaultAsync(beer => beer.Id == id);
}