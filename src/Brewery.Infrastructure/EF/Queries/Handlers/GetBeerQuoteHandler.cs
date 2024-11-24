using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Queries.Handlers;

public class GetBeerQuoteHandler : IQueryHandler<GetBeerQuote, BeerQuoteDto>
{
    private readonly DbSet<BeerQuote> _beerQuotes;

    public GetBeerQuoteHandler(BreweryDbContext dbContext)
    {
        _beerQuotes = dbContext.BeerQuotes;
    }
    public async Task<BeerQuoteDto> QueryAsync(GetBeerQuote query)
    {
        var beerQuote = await _beerQuotes
            .AsNoTracking()
            .Include(b => b.BeerOrders)
            .SingleOrDefaultAsync(b => b.Id == query.BeerQuoteId);

        return beerQuote is not null
            ? beerQuote.AsDto()
            : null;
    }
}