namespace Brewery.Domain.Entities;

public class Wholesaler
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    private readonly HashSet<Sale> _beers = new();
    public IEnumerable<Sale> Beers => _beers;

    public Wholesaler(Guid id)
    {
        Id = id;
    }
    
    public void ChangeName(string name)
        => Name = name;

    public void AddBeerSale(Sale sale)
    {
        _beers.Add(sale);
    }

    public void RemoveBeerSale(Sale sale)
    {
        _beers.Remove(sale);
    }

    public static Wholesaler Create(Guid id, string name)
    {
        var wholesaler = new Wholesaler(id);
        wholesaler.ChangeName(name);
        
        return wholesaler;
    }
}