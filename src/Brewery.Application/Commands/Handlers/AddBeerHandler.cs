using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBeerHandler : ICommandHandler<AddBeer>
{
    private readonly IBeerRepository _beerRepository;
    private readonly IBrewerRepository _brewerRepository;
    
    public AddBeerHandler(IBeerRepository beerRepository,
        IBrewerRepository brewerRepository)
    {
        _beerRepository = beerRepository;
        _brewerRepository = brewerRepository;
    }

    public async Task HandleAsync(AddBeer command)
    {
        var brewer = await _brewerRepository.GetBrewer(command.BrewerId);
        if (brewer is null)
        {
            throw new BrewerNotFoundException(command.BrewerId);
        }
        
        var beer = await _beerRepository.GetBeerById(command.Id);
        if (beer is not null)
        {
            throw new BeerAlreadyExistException(command.Id);
        }
        
        beer = Beer.Create(command.Id, brewer.Id, command.Name);
        
        await _beerRepository.AddAsync(beer);
    }
}