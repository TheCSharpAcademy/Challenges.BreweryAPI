namespace Brewery.Domain.Entities;

public class BeerOrder
{
    public Guid BeerId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }

    public BeerOrder(Guid beerId, int quantity, decimal unitPrice)
    {
        BeerId = beerId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Total = quantity * unitPrice;
    }
}