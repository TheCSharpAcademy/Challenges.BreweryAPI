using Brewery.Domain.Exceptions;

namespace Brewery.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public Guid BeerId { get; private set; }
    public int Quantity { get; private set; }

    public Sale(Guid id, Guid beerId)
    {
        Id = id;
        BeerId = beerId;
    }

    public void RestockBeer(Guid beerId, int quantity)
    {
        if (beerId != BeerId)
        {
            throw new InvalidBeerToBeRestockedException(beerId);
        }

        if (quantity <= 0)
        {
            throw new InvalidBeerQuantityException(quantity);
        }
        
        Quantity += quantity;
    }

    public void TakeBeer(int quantity)
    {
        if (Quantity < quantity)
        {
            throw new NotEnoughBeerToSellException(quantity);
        }
        
        Quantity -= quantity;
    }

    public static Sale Create(Guid id, Guid beerId, int quantity)
    {
        var sale = new Sale(id, beerId);
        sale.RestockBeer(beerId, quantity);
        
        return sale;
    }
}