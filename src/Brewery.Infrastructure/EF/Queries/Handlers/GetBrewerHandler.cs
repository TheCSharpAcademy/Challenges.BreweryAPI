using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Queries.Handlers;

public class GetBrewerHandler : IQueryHandler<GetBrewer, BrewerDto>
{
    private readonly DbSet<Brewer> _brewers;

    public GetBrewerHandler(BreweryDbContext dbContext)
    {
        _brewers = dbContext.Brewers;
    }
    public async Task<BrewerDto> QueryAsync(GetBrewer query)
    {
        var brewer = await _brewers
            .AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == query.BrewerId);

        return brewer is not null
            ? brewer.AsDto()
            : null;
    }
}