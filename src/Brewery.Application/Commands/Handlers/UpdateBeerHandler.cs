using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public record UpdateBeerHandler : ICommandHandler<UpdateBeer>
{
    private readonly IBeerRepository _beerRepository;

    public UpdateBeerHandler(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task HandleAsync(UpdateBeer command)
    {
        var beer = await _beerRepository.GetBeerById(command.Id);
        if (beer is null)
        {
            throw new BeerNotFoundException(command.Id);
        }

        if (!string.IsNullOrEmpty(command.Name))
        {
            beer.ChangeName(command.Name);
        }

        if (command.UnitPrice != 0)
        {
            beer.SetPrice(command.UnitPrice);
        }

        await _beerRepository.UpdateAsync(beer);
    }
}

