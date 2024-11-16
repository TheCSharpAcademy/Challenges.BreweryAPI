using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface IBeerSaleRepository
{
    Task AddAsync(BeerSale beerSale);
    Task UpdateAsync(BeerSale beerSale);
    Task DeleteAsync(BeerSale beerSale);
    Task<IEnumerable<BeerSale>> BrowseByBeerId(Guid beerId);
}