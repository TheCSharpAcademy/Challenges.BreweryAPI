using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface ISaleRepository
{
    Task AddAsync(BeerSale beerSale);
    Task UpdateAsync(BeerSale beerSale);
    Task DeleteAsync(BeerSale beerSale);
    Task<BeerSale> GetSaleByBeerId(Guid beerId);
    
}