using Brewery.Abstractions.Exceptions;

namespace Brewery.Domain.Exceptions;

public class InvalidBeerQuantityException : BreweryException
{
    public int Quantity { get; }
    public InvalidBeerQuantityException(int quantity)
        : base($"Quantity of '{quantity}' is invalid.")
    {
        Quantity = quantity;
    }
}