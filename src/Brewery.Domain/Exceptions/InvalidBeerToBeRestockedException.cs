using Brewery.Abstractions.Exceptions;

namespace Brewery.Domain.Exceptions;

public class InvalidBeerToBeRestockedException : BreweryException
{
    public Guid BeerId { get; }
    public InvalidBeerToBeRestockedException(Guid beerId)
        : base($"Beer with '{beerId}' is invalid to be restocked.")
    {
        BeerId = beerId;
    }
}