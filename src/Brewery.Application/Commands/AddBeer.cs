using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddBeer(Guid BrewerId, string Name) : ICommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
}