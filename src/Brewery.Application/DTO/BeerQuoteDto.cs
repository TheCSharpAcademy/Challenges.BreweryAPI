namespace Brewery.Application.DTO;

public class BeerQuoteDto
{
    public Guid Id { get; set; }
    public IEnumerable<BeerOrderDto> BeerOrders { get; set; }
    public int DiscountInPercent { get; set; }
    public decimal Total { get; set; }
}