using Brewery.Domain.Entities;

namespace Brewery.Application.DTO;

public static class Extensions
{
    public static BeerDto AsDto(this Beer beer)
    {
        var beerDto = new BeerDto
        {
            Id = beer.Id,
            BrewerId = beer.BrewerId,
            Name = beer.Name,
            UnitPrice = beer.UnitPrice,
        };
        
        return beerDto;
    }

    public static BrewerDto AsDto(this Brewer brewer)
    {
        var brewerDto = new BrewerDto
        {
            Id = brewer.Id,
            Name = brewer.Name,
        };
        
        return brewerDto;
    }

    public static WholesalerDto AsDto(this Wholesaler wholesaler)
        => new WholesalerDto
        {
            Id = wholesaler.Id,
            Name = wholesaler.Name,
        };
}
