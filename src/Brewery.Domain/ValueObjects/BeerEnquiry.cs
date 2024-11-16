namespace Brewery.Domain.ValueObjects;

public class BeerEnquiry
{
    public Guid BeerId { get; set; }
    public int RequiredQuantity { get; set; }
}