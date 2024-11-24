using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface IBeerQuoteRepository
{
    Task AddAsync(BeerQuote beerQuote);
    Task<BeerQuote> GetBeerQuote(Guid id);
}