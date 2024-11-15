using Brewery.Abstractions.Commands;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBeerStockHandler : ICommandHandler<AddBeerStock>
{
    private readonly IBeerStockRepository _beerStockRepository;
    private readonly IBreweryRepository _breweryRepository;
    private readonly IBeerRepository _beerRepository;

    public AddBeerStockHandler(IBeerStockRepository beerStockRepository,
        IBreweryRepository breweryRepository,
        IBeerRepository beerRepository)
    {
        _beerStockRepository = beerStockRepository;
        _breweryRepository = breweryRepository;
        _beerRepository = beerRepository;
    }

    public Task HandleAsync(AddBeerStock command)
    {
        throw new NotImplementedException();
    }
}