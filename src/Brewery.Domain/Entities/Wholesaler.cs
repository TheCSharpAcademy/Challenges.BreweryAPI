namespace Brewery.Domain.Entities;

public class Wholesaler
{
    private readonly HashSet<Beer> _beers = new();
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<Beer> Beers => _beers;
    
}