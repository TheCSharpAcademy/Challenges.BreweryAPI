using Brewery.Abstractions.Exceptions;

namespace Brewery.Domain.Exceptions;

public class NotEnoughBeerToTakeException : BreweryException
{
    public NotEnoughBeerToTakeException(int quantity)
        : base($"Not enough beer to take. Quantity of '{quantity}' is excessive.")
    {
    }
}