using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BeerDoesNotBelongToBrewerException : BreweryException
{
    public Guid BeerId { get; }
    public Guid BrewerId { get; }
    public BeerDoesNotBelongToBrewerException(Guid beerId, Guid brewerId)
        : base($"Beer with id {beerId} does not belong to brewer with id '{brewerId}'")
    {
        BeerId = beerId;
        BrewerId = brewerId;
    }
}