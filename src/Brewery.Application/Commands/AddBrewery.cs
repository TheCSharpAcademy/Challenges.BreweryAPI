using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddBrewery(Guid Id, string Name) : ICommand;