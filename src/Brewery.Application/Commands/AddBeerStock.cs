using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddBeerStock(Guid BeerId, Guid BrewerId, int Quantity, decimal UnitPrice) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}