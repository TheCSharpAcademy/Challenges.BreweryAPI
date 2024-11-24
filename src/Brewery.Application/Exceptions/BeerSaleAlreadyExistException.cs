using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BeerSaleAlreadyExistException : BreweryException
{
    public Guid WholesalerId { get; }
    public Guid BeerId { get; }
    public BeerSaleAlreadyExistException(Guid wholesalerId, Guid beerId)
        : base($"Wholsaler with id '{wholesalerId}' already offers beer sale with id '{beerId}'.")
    {
        WholesalerId = wholesalerId;
        BeerId = beerId;
    }
}