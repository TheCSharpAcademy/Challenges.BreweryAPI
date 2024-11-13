using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record UpdateBeer(Guid Id, string Name, decimal UnitPrice) : ICommand;