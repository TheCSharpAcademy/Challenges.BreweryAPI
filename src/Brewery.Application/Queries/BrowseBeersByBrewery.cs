using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;

namespace Brewery.Application.Queries;

public record BrowseBeersByBrewery(Guid BreweryId) : IQuery<IEnumerable<BeerDto>>;