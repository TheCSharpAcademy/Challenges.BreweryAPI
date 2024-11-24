namespace Brewery.Domain.Repositories;

public interface IBreweryRepository
{
    Task AddBrewery(Entities.Brewery brewery);
    Task UpdateBrewery(Entities.Brewery brewery);
    Task DeleteBrewery(Entities.Brewery brewery);
    Task<Entities.Brewery> GetBreweryById(Guid breweryId);
}