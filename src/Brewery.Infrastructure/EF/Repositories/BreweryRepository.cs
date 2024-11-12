using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class BreweryRepository : IBreweryRepository
{
    private readonly DbSet<Domain.Entities.Brewery> _breweries;
    private readonly BreweryDbContext _dbContext;

    public BreweryRepository(BreweryDbContext dbContext)
    {
        _breweries = dbContext.Breweries;
        _dbContext = dbContext;
    }
    
    public async Task AddBrewery(Domain.Entities.Brewery brewery)
    {
        await _breweries.AddAsync(brewery);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBrewery(Domain.Entities.Brewery brewery)
    {
        _breweries.Update(brewery);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBrewery(Domain.Entities.Brewery brewery)
    {
        _breweries.Remove(brewery);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Domain.Entities.Brewery> GetBreweryById(Guid breweryId)
        => _breweries
            .SingleOrDefaultAsync(b => b.Id == breweryId);
}