namespace Brewery.Abstractions.Exceptions;

public class BreweryException : Exception
{
    public string Message { get; set; }
    
    public BreweryException(string message)
    {
        Message = message;
    }
}