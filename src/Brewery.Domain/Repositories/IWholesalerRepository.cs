using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface IWholesalerRepository
{
    Task AddWholesaler(Wholesaler wholesaler);
    Task UpdateWholesaler(Wholesaler wholesaler);
    Task<Wholesaler> GetWholesaler(Guid id);
}