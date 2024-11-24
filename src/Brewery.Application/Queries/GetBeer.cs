using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;

namespace Brewery.Application.Queries;

public record GetBeer(Guid Id) : IQuery<BeerDto>;