using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BeerAlreadyExistException : BreweryException
{
    public Guid BeerId { get; }
    public BeerAlreadyExistException(Guid beerId) 
        : base($"Beer with id '{beerId}' already exists.")
    {
        BeerId = beerId;
    }
}