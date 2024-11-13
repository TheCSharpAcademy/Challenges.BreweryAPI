using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class DeleteBeerHandler : ICommandHandler<DeleteBeer>
{
    private readonly IBeerRepository _beerRepositry;

    public DeleteBeerHandler(IBeerRepository beerRepositry)
    {
        _beerRepositry = beerRepositry;
    }

    public async Task HandleAsync(DeleteBeer command)
    {
        var beer = await _beerRepositry.GetBeerById(command.Id);
        if (beer is null)
        {
            throw new BeerNotFoundException(command.Id);
        }
        
        await _beerRepositry.DeleteAsync(beer);
    }
}