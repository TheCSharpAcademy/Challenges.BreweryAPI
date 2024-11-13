using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBeerHandler : ICommandHandler<AddBeer>
{
    private readonly IBeerRepository _beerRepository;

    public AddBeerHandler(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task HandleAsync(AddBeer command)
    {
        var beer = await _beerRepository.GetBeerById(command.Id);
        if (beer is not null)
        {
            throw new BeerAlreadyExistException(command.Id);
        }
        
        beer = Beer.Create(command.Id, command.BrewerId, command.Name, command.UnitPrice);
        
        await _beerRepository.AddAsync(beer);
    }
}