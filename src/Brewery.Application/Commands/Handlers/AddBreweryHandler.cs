using Brewery.Abstractions.Commands;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBreweryHandler : ICommandHandler<AddBrewery>
{
    private readonly IBreweryRepository _breweryRepository;

    public AddBreweryHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task HandleAsync(AddBrewery command)
    {
        var brewery = await _breweryRepository.GetBreweryById(command.Id);
        if (brewery is not null)
        {
            
        }
        
        brewery = Domain.Entities.Brewery.Create(command.Id, command.Name);
        await _breweryRepository.AddBrewery(brewery);
    }
}