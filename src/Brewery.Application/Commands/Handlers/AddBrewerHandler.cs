using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBrewerHandler : ICommandHandler<AddBrewer>
{
    private readonly IBrewerRepository _brewerRepository;

    public AddBrewerHandler(IBrewerRepository brewerRepository)
    {
        _brewerRepository = brewerRepository;
    }

    public async Task HandleAsync(AddBrewer command)
    {
        var brewer = await _brewerRepository.GetBrewer(command.Id);
        if (brewer is not null)
        {
            throw new BrewerAlreadyExistException(command.Id);
        }
        
        brewer = Brewer.Create(command.Id, command.Name);
        await _brewerRepository.AddBrewer(brewer);
    }
}