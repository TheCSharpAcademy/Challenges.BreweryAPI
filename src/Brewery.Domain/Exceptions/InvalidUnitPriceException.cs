using Brewery.Abstractions.Exceptions;

namespace Brewery.Domain.Exceptions;

public class InvalidUnitPriceException : BreweryException
{
    public decimal Price { get; }
    public InvalidUnitPriceException(decimal price)
        : base($"Unit Price of '{price}' is invalid.")
    {
        Price = price;
    }
}
