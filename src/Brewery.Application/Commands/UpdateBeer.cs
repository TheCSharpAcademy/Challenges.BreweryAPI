using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record UpdateBeer(Guid Id, string Name = null, decimal UnitPrice = default) : ICommand;