using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DbSet<Sale> _sales;
    private readonly BreweryDbContext _dbContext;

    public SaleRepository(BreweryDbContext dbContext)
    {
        _sales = _dbContext.Sales;
        _dbContext = dbContext;
    }

    public async Task AddAsync(Sale sale)
    {
        await _sales.AddAsync(sale);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Sale sale)
    {
        _sales.Remove(sale);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Sale> GetSaleByBeerId(Guid beerId)
        => _sales.SingleOrDefaultAsync(s => s.BeerId == beerId);
}