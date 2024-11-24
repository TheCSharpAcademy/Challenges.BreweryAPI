using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;

namespace Brewery.Application.Queries;

public record GetBrewery(Guid Id) : IQuery<BreweryDto>;