using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface ISaleRepository
{
    Task AddAsync(Sale sale);
    Task DeleteAsync(Sale sale);
    Task<Sale> GetSaleByBeerId(Guid beerId);
    
}