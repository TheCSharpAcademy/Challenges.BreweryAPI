namespace Brewery.Domain.Entities;

public class Wholesaler
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    private readonly HashSet<BeerSale> _beerSales = new();
    public IEnumerable<BeerSale> BeerSales => _beerSales;

    public Wholesaler(Guid id)
    {
        Id = id;
    }
    
    public void ChangeName(string name)
        => Name = name;

    public void AddBeerSale(BeerSale beerSale)
    {
        _beerSales.Add(beerSale);
    }

    public void RemoveBeerSale(BeerSale beerSale)
    {
        _beerSales.Remove(beerSale);
    }

    public static Wholesaler Create(Guid id, string name)
    {
        var wholesaler = new Wholesaler(id);
        wholesaler.ChangeName(name);
        
        return wholesaler;
    }
}