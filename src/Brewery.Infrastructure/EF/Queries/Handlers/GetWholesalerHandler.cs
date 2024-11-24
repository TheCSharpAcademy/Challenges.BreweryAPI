using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Queries.Handlers;

public class GetWholesalerHandler : IQueryHandler<GetWholesaler, WholesalerDto>
{
    private readonly DbSet<Wholesaler> _wholesalers;

    public GetWholesalerHandler(BreweryDbContext dbContext)
    {
        _wholesalers = dbContext.Wholesalers;
    }
    public async Task<WholesalerDto> QueryAsync(GetWholesaler query)
    {
        var wholesaler = await _wholesalers
            .AsNoTracking()
            .SingleOrDefaultAsync(w => w.Id == query.WholesalerId);
        
        return wholesaler is not null
            ? wholesaler.AsDto()
            : null;
    }
}