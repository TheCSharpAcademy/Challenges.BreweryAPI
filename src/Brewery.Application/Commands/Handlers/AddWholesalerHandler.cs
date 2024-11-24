using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddWholesalerHandler : ICommandHandler<AddWholesaler>
{
    private readonly IWholesalerRepository _wholesalerRepository;

    public AddWholesalerHandler(IWholesalerRepository wholesalerRepository)
    {
        _wholesalerRepository = wholesalerRepository;
    }

    public async Task HandleAsync(AddWholesaler command)
    {
        var wholesaler = await _wholesalerRepository.GetWholesaler(command.Id);
        if (wholesaler is not null)
        {
            throw new WholesaleAlreadyExistException(command.Id);
        }
        
        wholesaler = Wholesaler.Create(command.Id, command.Name);
        await _wholesalerRepository.AddWholesaler(wholesaler);
    }
}