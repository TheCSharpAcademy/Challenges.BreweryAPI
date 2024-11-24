using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class BeerQuoteRepository : IBeerQuoteRepository
{
    private readonly DbSet<BeerQuote> _beerQuotes;
    private readonly BreweryDbContext _dbContext;

    public BeerQuoteRepository(BreweryDbContext dbContext)
    {
        _beerQuotes = dbContext.BeerQuotes;
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(BeerQuote beerQuote)
    {
        await _beerQuotes.AddAsync(beerQuote);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<BeerQuote> GetBeerQuote(Guid id)
        => await _beerQuotes.SingleOrDefaultAsync(beerQuote => beerQuote.Id == id);
}