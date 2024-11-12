using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;

namespace Brewery.Application.Queries;

public record BrowseBeers(Guid BreweryId) : IQuery<IEnumerable<BeerDto>>;