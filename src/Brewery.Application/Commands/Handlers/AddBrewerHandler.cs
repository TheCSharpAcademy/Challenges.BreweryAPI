using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBrewerHandler : ICommandHandler<AddBrewer>
{
    private readonly IBrewerRepository _brewerRepository;
    private readonly IBreweryRepository _breweryRepository;

    public AddBrewerHandler(IBrewerRepository brewerRepository,
        IBreweryRepository breweryRepository)
    {
        _brewerRepository = brewerRepository;
        _breweryRepository = breweryRepository;
    }

    public async Task HandleAsync(AddBrewer command)
    {
        var brewer = await _brewerRepository.GetBrewer(command.Id);
        if (brewer is not null)
        {
            throw new BrewerAlreadyExistException(command.Id);
        }

        brewer = Brewer.Create(command.Id, command.Name);
        if (command.BreweryId != Guid.Empty)
        {
            var brewery = await _breweryRepository.GetBreweryById(command.BreweryId);
            if (brewery is null)
            {
                throw new BreweryNotFoundException(command.BreweryId);
            }

            brewer.ChangeBreweryId(brewery.Id);
        }
        
        await _brewerRepository.AddBrewer(brewer);
    }
}