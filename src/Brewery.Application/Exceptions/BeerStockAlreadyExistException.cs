using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BeerStockAlreadyExistException : BreweryException
{
    public Guid BeerId { get; }
    public BeerStockAlreadyExistException(Guid beerId)
        : base($"Beer stock for beer with id '{beerId}' already exists.")
    {
        BeerId = beerId;
    }
}