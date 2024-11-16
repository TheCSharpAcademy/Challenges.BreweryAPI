using Brewery.Abstractions.Exceptions;
using Brewery.Domain.Exceptions;

namespace Brewery.Domain.Entities;

public class Beer
{
    public Guid Id { get; private set; }
    public Guid BrewerId { get; private set; }
    public string Name { get; private set; }

    public Beer(Guid id, Guid brewerId)
    {
        Id = id;
        BrewerId = brewerId;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }
    
    public static Beer Create(Guid id, Guid brewerId, string name, decimal unitPrice)
    {
        var beer = new Beer(id, brewerId);
        beer.ChangeName(name);

        return beer;
    }
}