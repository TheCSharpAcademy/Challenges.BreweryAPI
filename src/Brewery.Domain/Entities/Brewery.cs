namespace Brewery.Domain.Entities;

public class Brewery
{
    private readonly List<Brewer> _brewers = new List<Brewer>();
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<Brewer> Brewers => _brewers;

    public Brewery(Guid id)
    {
        Id = id;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void AddBrewer(Brewer brewer)
    {
        _brewers.Add(brewer);
    }

    public void RemoveBrewer(Brewer brewer)
    {
        _brewers.Remove(brewer);
    }

    public static Brewery Create(Guid id, string name)
    {
        var brewery = new Brewery(id);
        brewery.ChangeName(name);
        
        return brewery;
    }

}