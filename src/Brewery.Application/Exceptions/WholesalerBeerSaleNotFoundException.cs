using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class WholesalerBeerSaleNotFoundException : BreweryException
{
    public Guid WholesalerId { get; }
    public Guid BeerId { get; }
    public WholesalerBeerSaleNotFoundException(Guid wholesalerId, Guid beerId)
        : base($"Beer sale was not found. Wholesaler with id '{wholesalerId}' does not offer beer sale with id '{beerId}'.")
    {
        WholesalerId = wholesalerId;
        BeerId = beerId;
    }
}