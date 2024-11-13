using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BrewerAlreadyExistException : BreweryException
{
    public Guid BrewerId { get; }
    public BrewerAlreadyExistException(Guid brewerId) 
        : base($"Brewer with id '{brewerId}' already exists.")
    {
        BrewerId = brewerId;
    }
}