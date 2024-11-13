using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddBeer(Guid BrewerId, string Name, decimal UnitPrice) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}