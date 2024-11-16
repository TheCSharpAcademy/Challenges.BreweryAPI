namespace Brewery.Application.DTO;

public class BeerOrderDto
{
    public Guid BeerId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}