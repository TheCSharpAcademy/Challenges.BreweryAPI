using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BrewerNotFoundException : BreweryException
{
    public Guid BrewerId { get; }
    public BrewerNotFoundException(Guid brewerId)
        : base($"Brewer with id '{brewerId}' was not found.")
    {
        BrewerId = brewerId;
    }
}