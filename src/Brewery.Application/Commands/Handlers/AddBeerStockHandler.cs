using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBeerStockHandler : ICommandHandler<AddBeerStock>
{
    private readonly IBeerStockRepository _beerStockRepository;
    private readonly IBrewerRepository _brewerRepository;
    private readonly IBeerRepository _beerRepository;

    public AddBeerStockHandler(IBeerStockRepository beerStockRepository,
        IBrewerRepository brewerRepository,
        IBeerRepository beerRepository)
    {
        _beerStockRepository = beerStockRepository;
        _brewerRepository = brewerRepository;
        _beerRepository = beerRepository;
    }

    public async Task HandleAsync(AddBeerStock command)
    {
        var brewer = await _brewerRepository.GetBrewer(command.BrewerId);
        if (brewer is null)
        {
            throw new BrewerNotFoundException(command.BrewerId);
        }
        
        var beer = await _beerRepository.GetBeerById(command.BeerId);
        if (beer is null)
        {
            throw new BeerNotFoundException(command.BeerId);
        }
        
        var beerStock = await _beerStockRepository.GetBeerStock(command.BeerId);
        if (beerStock is not null)
        {
            throw new BeerStockAlreadyExistException(command.BeerId);
        }
        
        beerStock = BeerStock.Create(Guid.NewGuid(), brewer.Id, beer.Id, 
            command.Quantity, command.UnitPrice);
        await _beerStockRepository.AddBeerStock(beerStock);
    }
}