using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Queries.Handlers;

public class BrowseBeersByBreweryHandler : IQueryHandler<BrowseBeersByBrewery, IEnumerable<BeerDto>>
{
    private readonly DbSet<Domain.Entities.Brewery> _breweries;

    public BrowseBeersByBreweryHandler(BreweryDbContext dbContext)
    {
        _breweries = dbContext.Breweries;
    }
    public async Task<IEnumerable<BeerDto>> QueryAsync(BrowseBeersByBrewery query)
    {
        throw new NotImplementedException();
        // var brewery = await _breweries
        //     .AsNoTracking()
        //     //.Include(b => b.Beers)
        //     .SingleOrDefaultAsync(b => b.Id == query.BreweryId);
        //
        // return brewery is not null 
        //     ? brewery?.Beers.Select(b => b.AsDto())
        //     : Enumerable.Empty<BeerDto>();
    }
}