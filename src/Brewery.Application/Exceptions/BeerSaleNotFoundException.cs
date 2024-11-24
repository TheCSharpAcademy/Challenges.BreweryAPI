using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BeerSaleNotFoundException : BreweryException
{
    public Guid BeerId { get; }
    public BeerSaleNotFoundException(Guid beerId)
        : base($"Sale for beer with id '{beerId}' was not found.")
    {
        BeerId = beerId;
    }
}