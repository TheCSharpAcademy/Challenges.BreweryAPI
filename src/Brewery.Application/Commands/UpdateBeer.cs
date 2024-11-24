using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record UpdateBeer(Guid Id, Guid BrewerId, string Name = null, decimal UnitPrice = default) : ICommand;