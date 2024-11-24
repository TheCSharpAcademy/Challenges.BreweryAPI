using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Queries.Handlers;

public class GetBeerHandler : IQueryHandler<GetBeer, BeerDto>
{
    private readonly DbSet<Beer> _beers;

    public GetBeerHandler(BreweryDbContext dbContext)
    {
        _beers = dbContext.Beers;
    }

    public async Task<BeerDto> QueryAsync(GetBeer query)
    {
        var beer = await _beers.AsNoTracking()
            .SingleOrDefaultAsync(beer => beer.Id == query.Id);

        return beer is not null
            ? beer.AsDto()
            : null;
    }
}