using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class WholesalerRepository : IWholesalerRepository
{
    private readonly DbSet<Wholesaler> _wholesalers;
    private readonly BreweryDbContext _dbContext;

    public WholesalerRepository(BreweryDbContext dbContext)
    {
        _wholesalers = dbContext.Wholesalers;
        _dbContext = dbContext;
    }

    public async Task AddWholesaler(Wholesaler wholesaler)
    {
        await _wholesalers.AddAsync(wholesaler);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateWholesaler(Wholesaler wholesaler)
    {
        _wholesalers.Update(wholesaler);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Wholesaler> GetWholesaler(Guid id)
        => _wholesalers.SingleOrDefaultAsync(w => w.Id == id);
}