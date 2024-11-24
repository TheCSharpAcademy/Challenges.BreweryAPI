using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddWholesaler(string Name) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}