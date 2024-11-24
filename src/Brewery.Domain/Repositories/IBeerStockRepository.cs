using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface IBeerStockRepository
{
    Task AddBeerStock(BeerStock stock);
    Task UpdateBeerStock(BeerStock stock);
    Task DeleteBeerStock(BeerStock stock);
    Task<BeerStock> GetBeerStock(Guid beerId);
}