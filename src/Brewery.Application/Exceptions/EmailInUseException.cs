using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class EmailInUseException : BreweryException
{
    public string Email { get; }
    public EmailInUseException(string email)
        : base($"Email '{email}' already in use.")
    {
        Email = email;
    }
}