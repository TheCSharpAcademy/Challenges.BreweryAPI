using Brewery.Domain.Exceptions;

namespace Brewery.Domain.Entities;

public class BeerStock
{
    public Guid Id { get; private set; }
    public Guid BeerId { get; private set; }
    public int Quantity { get; private set; }
    
    public BeerStock(Guid id, Guid beerId)
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

    public void TakeForBeerSale(int quantity)
    {
        if (Quantity < quantity)
        {
            throw new NotEnoughBeerToTakeException(quantity);
        }
        
        Quantity -= quantity;
    }

    public static BeerStock Create(Guid id, Guid beerId, int quantity)
    {
        var beerStock = new BeerStock(id, beerId);
        beerStock.RestockBeer(beerId, quantity);
        
        return beerStock;
    }
}