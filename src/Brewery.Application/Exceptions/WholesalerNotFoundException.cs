using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class WholesalerNotFoundException : BreweryException
{
    public Guid WholesalerId { get; }
    public WholesalerNotFoundException(Guid wholesalerId) 
        : base($"Wholesaler with id '{wholesalerId}' was not found.")
    {
        WholesalerId = wholesalerId;
    }
}