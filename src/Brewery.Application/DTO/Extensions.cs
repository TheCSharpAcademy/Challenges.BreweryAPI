using Brewery.Domain.Entities;
using Brewery.Domain.ValueObjects;

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

    public static BeerOrderDto AsDto(this BeerOrder beerOrder)
        => new BeerOrderDto
        {
            BeerId = beerOrder.BeerId,
            Quantity = beerOrder.Quantity,
            UnitPrice = beerOrder.UnitPrice,
            Total = beerOrder.Total,
        };

    public static BeerQuoteDto AsDto(this BeerQuote beerQuote)
        => new BeerQuoteDto()
        {
            Id = beerQuote.Id,
            BeerOrders = beerQuote.BeerOrders
                .Select(bo => bo.AsDto()),
            Total = beerQuote.Total,
            DiscountInPercent = beerQuote.DiscountInPercent,
        };
}
