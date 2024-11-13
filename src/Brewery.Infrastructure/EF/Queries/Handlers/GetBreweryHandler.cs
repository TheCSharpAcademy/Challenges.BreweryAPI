using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Queries.Handlers;

public class GetBreweryHandler : IQueryHandler<GetBrewery, BreweryDto>
{
    private readonly DbSet<Domain.Entities.Brewery> _breweries;

    public GetBreweryHandler(BreweryDbContext dbContext)
    {
        _breweries = dbContext.Breweries;
    }

    public async Task<BreweryDto> QueryAsync(GetBrewery query)
    {
        var brewery = await _breweries
            .AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == query.Id);

        return brewery is not null
            ? new BreweryDto
            {
                Id = brewery.Id,
                Name = brewery.Name,
            }
            : null;
    }
}