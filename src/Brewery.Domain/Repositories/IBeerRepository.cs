using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface IBeerRepository
{
    Task AddAsync(Beer beer);
    Task UpdateAsync(Beer beer);
    Task DeleteAsync(Beer beer);
    Task<Beer> GetBeerById(Guid id);
}