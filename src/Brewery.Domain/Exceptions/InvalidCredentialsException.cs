using Brewery.Abstractions.Exceptions;

namespace Brewery.Domain.Exceptions;

public class InvalidCredentialsException : BreweryException
{
    public InvalidCredentialsException() : base($"Invalid sign-in credentials.")
    {
        
    }
}