using Brewery.Abstractions.Exceptions;

namespace Brewery.Domain.Exceptions;

public class NotEnoughBeerToSellException : BreweryException
{
    public NotEnoughBeerToSellException(int quantity)
        : base($"Not enough beer to sell. Quantity of '{quantity}' is excessive.")
    {
    }
}