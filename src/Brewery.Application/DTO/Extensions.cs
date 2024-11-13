using Brewery.Domain.Entities;

namespace Brewery.Application.DTO;

public static class Extensions
{
    public static BeerDto AsDto(this Beer beer)
    {
        var beerDto = new BeerDto
        {
            Id = beer.Id,
            Name = beer.Name,
        };
        
        return beerDto;
    }
}
