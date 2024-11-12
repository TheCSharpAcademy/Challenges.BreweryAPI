using Brewery.Abstractions.Exceptions;
using Brewery.Domain.Exceptions;

namespace Brewery.Domain.Entities;

public class Beer
{
    public Guid Id { get; private set; }
    public Guid BrewerId { get; private set; }
    public decimal UnitPrice { get; private set; }

    public Beer(Guid id, Guid brewerId)
    {
        Id = id;
    }
    
    public void SetPrice(decimal unitPrice)
    {
        if (unitPrice <= 0)
        {
            throw new InvalidUnitPriceException(unitPrice);
        }
        
        UnitPrice = unitPrice;
    }

    public static Beer Create(Guid id, Guid brewerId)
    {
        var beer = new Beer(id, brewerId);

        return beer;
    }
}