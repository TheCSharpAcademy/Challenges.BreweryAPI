using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public record UpdateBeerHandler : ICommandHandler<UpdateBeer>
{
    private readonly IBeerRepository _beerRepository;
    private readonly IBrewerRepository _brewerRepository;

    public UpdateBeerHandler(IBeerRepository beerRepository,
        IBrewerRepository brewerRepository)
    {
        _beerRepository = beerRepository;
        _brewerRepository = brewerRepository;
    }

    public async Task HandleAsync(UpdateBeer command)
    {
        var brewer = await _brewerRepository.GetBrewer(command.BrewerId);
        if (brewer is null)
        {
            throw new BrewerNotFoundException(command.BrewerId);
        }

        var beer = await _beerRepository.GetBeerById(command.Id);
        if (beer is null)
        {
            throw new BeerNotFoundException(command.Id);
        }

        if (beer.BrewerId != brewer.Id)
        {
            throw new BeerDoesNotBelongToBrewerException(beer.Id, brewer.Id);
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

