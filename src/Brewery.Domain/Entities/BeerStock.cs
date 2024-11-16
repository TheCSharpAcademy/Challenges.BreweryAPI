using Brewery.Domain.Exceptions;

namespace Brewery.Domain.Entities;

public class BeerStock
{
    public Guid Id { get; private set; }
    public Guid BeerId { get; private set; }
    public Guid BrewerId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    
    public BeerStock(Guid id, Guid brewerId, Guid beerId)
    {
        Id = id;
        BrewerId = brewerId;
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
    
    public void SetPrice(decimal unitPrice)
    {
        if (unitPrice <= 0)
        {
            throw new InvalidUnitPriceException(unitPrice);
        }
        
        UnitPrice = unitPrice;
    }

    public static BeerStock Create(Guid id, Guid brewerId, Guid beerId, int quantity, decimal unitPrice)
    {
        var beerStock = new BeerStock(id, brewerId, beerId);
        beerStock.RestockBeer(beerId, quantity);
        beerStock.SetPrice(unitPrice);
        
        return beerStock;
    }
}