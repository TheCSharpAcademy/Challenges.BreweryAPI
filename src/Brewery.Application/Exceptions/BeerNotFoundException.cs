using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BeerNotFoundException : BreweryException
{
    public Guid BeerId { get; }
    public BeerNotFoundException(Guid beerId)
        : base($"Beer with id '{beerId}' was not found.")
    {
        BeerId = beerId;
    }
}