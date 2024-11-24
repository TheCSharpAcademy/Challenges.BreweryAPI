using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class ExcessiveBeerSaleOrderQuantityException : BreweryException
{
    public int Quantity { get; }
    public ExcessiveBeerSaleOrderQuantityException(int quantity)
        : base($"Order quantity of '{quantity}' beers is excessive. Not enough stock.")
    {
        Quantity = quantity;
    }
}