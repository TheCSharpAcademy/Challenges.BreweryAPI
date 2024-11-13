namespace Brewery.Domain.Entities;

public class Brewer
{
    public IEnumerable<Beer> Beers => _beers;
    public Guid Id { get; private set; }
    public Guid? BreweryId { get; private set; }
    public string Name { get; private set; }
    private readonly List<Beer> _beers = new List<Beer>();

    public Brewer(Guid id)
    {
        Id = id;
    }
    
    public void ChangeName(string name)
        => Name = name;
    
    public void AddBeer(Beer beer)
        => _beers.Add(beer);
    
    public void DeleteBeer(Beer beer)
        => _beers.Remove(beer);

    public void UpdateBeer(Beer beer)
    {
        var exisitingBeer = _beers.First(b => b.Id == beer.Id);
        
    }
    
    public static Brewer Create(Guid id, string name)
    {
        var brewer = new Brewer(id);
        brewer.ChangeName(name);
        
        return brewer;
    }
}