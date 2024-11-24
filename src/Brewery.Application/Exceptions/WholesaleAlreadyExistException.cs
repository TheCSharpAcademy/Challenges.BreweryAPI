using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class WholesaleAlreadyExistException : BreweryException
{
    public Guid WholesalerId { get; }
    public WholesaleAlreadyExistException(Guid wholesalerId)
        : base($"Wholesaler with id '{wholesalerId}' already exists.")
    {
        WholesalerId = wholesalerId;
    }
}