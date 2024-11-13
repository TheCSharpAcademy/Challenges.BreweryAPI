using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record DeleteBeer(Guid Id) : ICommand;