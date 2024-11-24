using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class OrderCannotBeEmptyException : BreweryException
{
    public Guid BeerId { get; }
    public OrderCannotBeEmptyException(Guid beerId)
        : base($"Beer order with id '{beerId}' cannot be empty.")
    {
        BeerId = beerId;
    }
}