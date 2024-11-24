using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class DeleteBeerHandler : ICommandHandler<DeleteBeer>
{
    private readonly IBeerRepository _beerRepository;

    public DeleteBeerHandler(IBeerRepository beerRepositry)
    {
        _beerRepository = beerRepositry;
    }

    public async Task HandleAsync(DeleteBeer command)
    {
        var beer = await _beerRepository.GetBeerById(command.BeerId);
        if (beer is null)
        {
            throw new BeerNotFoundException(command.BeerId);
        }

        if (beer.BrewerId != command.BrewerId)
        {
            throw new BeerDoesNotBelongToBrewerException(beer.Id, beer.BrewerId);
        }

        await _beerRepository.DeleteAsync(beer);
    }
}