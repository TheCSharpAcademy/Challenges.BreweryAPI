using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;

namespace Brewery.Application.Queries;

public record GetBrewer(Guid BrewerId) : IQuery<BrewerDto>;