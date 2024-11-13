using Brewery.Domain.Entities;

namespace Brewery.Application.DTO;

public class BeerDto
{
    public Guid Id { get; set; }
    public Guid BrewerId { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
}
