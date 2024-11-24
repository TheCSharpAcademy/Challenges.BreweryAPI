using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class NotEnoughBeerForRequestException : BreweryException
{
    public Guid BeerId { get; }
    public int Quantity { get; }
    public NotEnoughBeerForRequestException(Guid beerId, int quantity)
        : base($"Not enough beer with id '{beerId}'. Requested quantity of '{quantity}' is excessive.")
    {
        BeerId = beerId;
        Quantity = quantity;
    }
}