using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddBrewer(string Name) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}