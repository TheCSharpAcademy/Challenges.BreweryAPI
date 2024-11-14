using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public class AddWholesaler(string Name) : ICommand
{
    public Guid Id => Guid.NewGuid();
}