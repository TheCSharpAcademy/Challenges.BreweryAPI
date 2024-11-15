using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BeerStockNotEnoughToFulfilOrderException : BreweryException
{
    public Guid BeerId { get; }
    public int Quantity { get; }
    public BeerStockNotEnoughToFulfilOrderException(Guid beerId, int quantity)
        : base($"Beer stock for beer with id '{beerId}' is not enough. Sale quantity of '{quantity}' is excessive.")
    {
        BeerId = beerId;
        Quantity = quantity;
    }
}