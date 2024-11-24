using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BeerStockNotFoundException : BreweryException
{
    public Guid BeerId { get; }
    public BeerStockNotFoundException(Guid beerId) 
        : base($"Beer stock for beer with id '{beerId}' was not found.")
    {
        BeerId = beerId;
    }
}