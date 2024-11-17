using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddBrewer(string Name, Guid BreweryId) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}