using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class BrewerRepository : IBrewerRepository
{
    private readonly DbSet<Brewer> _brewers;
    private readonly BreweryDbContext _dbContext;

    public BrewerRepository(BreweryDbContext dbContext)
    {
        _brewers = dbContext.Brewers;
        _dbContext = dbContext;
    }
    
    public async Task AddBrewer(Brewer brewer)
    {
        await _brewers.AddAsync(brewer);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Brewer brewer)
    {
        _brewers.Update(brewer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Brewer brewer)
    {
        _brewers.Remove(brewer);
        await _dbContext.SaveChangesAsync();
    }
    
    public Task<Brewer> GetBrewer(Guid brewerId)
        => _brewers.SingleOrDefaultAsync(_brewers => _brewers.Id == brewerId);
}