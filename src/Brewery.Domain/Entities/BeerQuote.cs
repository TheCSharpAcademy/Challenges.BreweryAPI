using Brewery.Domain.ValueObjects;

namespace Brewery.Domain.Entities;

public class BeerQuote
{
    public Guid Id { get; private set; }
    public IEnumerable<BeerOrder> BeerOrders => _beerOrders;
    private readonly HashSet<BeerOrder> _beerOrders = new HashSet<BeerOrder>();
    public int DiscountInPercent { get; private set; }
    public decimal Total { get; private set; }

    public BeerQuote(Guid id)
    {
        Id = id;
    }
    
    public void AddBeerOrder(BeerOrder beerOrder)
        => _beerOrders.Add(beerOrder);

    public void CalculateTotal()
    {
        foreach (var beerOrder in _beerOrders)
        {
            Total += beerOrder.Total;
        }

        if (DiscountInPercent is not 0)
        {
            Total = Total * (100 - DiscountInPercent) / 100;
        }
    }

    public void CalculateDiscount()
    {
        var beerAmount = _beerOrders.Sum(b => b.Quantity);
        if (beerAmount > 20)
        {
            DiscountInPercent = 20;
            return;
        }

        if (beerAmount > 10)
        {
            DiscountInPercent = 10;
            return;
        }

        DiscountInPercent = 0;
        return;
    }

    public static BeerQuote Create(Guid id)
        => new BeerQuote(id);
}