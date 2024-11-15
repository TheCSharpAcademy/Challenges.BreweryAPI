using Brewery.Domain.Exceptions;

namespace Brewery.Domain.Entities;

public class BeerSale
{
    public Guid Id { get; private set; }
    public Guid BeerId { get; private set; }
    public int Quantity { get; private set; }

    public BeerSale(Guid id, Guid beerId)
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

    public void SellBeer(int quantity)
    {
        if (Quantity < quantity)
        {
            throw new NotEnoughBeerToTakeException(quantity);
        }
        
        Quantity -= quantity;
    }

    public static BeerSale Create(Guid id, Guid beerId, int quantity)
    {
        var sale = new BeerSale(id, beerId);
        sale.RestockBeer(beerId, quantity);
        
        return sale;
    }
}