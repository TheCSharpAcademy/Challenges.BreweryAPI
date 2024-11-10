namespace Brewery.Domain.Entities;

public class Beer
{
    public Guid Id { get; set; }
    public Guid BrewerId { get; set; }

    public Beer(Guid id, Guid brewerId)
    {
        Id = id;
    }
    
}