using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class DuplicatesInRequestQuoteException : BreweryException
{
    public DuplicatesInRequestQuoteException()
        : base($"Request quote cannot contain duplicates.")
    {
    }
}