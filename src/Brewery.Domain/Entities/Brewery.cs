namespace Brewery.Domain.Entities;

public class Brewery
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    // public Guid BrewerId { get; private set; }
    // public Brewer Brewer { get; private set; }

    public Brewery(Guid id)
    {
        Id = id;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }
    
    

    public static Brewery Create(Guid id, string name)
    {
        var brewery = new Brewery(id);
        brewery.ChangeName(name);
        
        return brewery;
    }

}