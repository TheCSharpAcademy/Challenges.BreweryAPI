using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddBrewer(string Name, Guid BreweryId = default) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}