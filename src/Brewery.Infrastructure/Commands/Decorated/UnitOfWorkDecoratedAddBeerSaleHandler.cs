using Brewery.Abstractions.Commands;
using Brewery.Application.Commands;
using Brewery.Infrastructure.EF.UnitOfWork;

namespace Brewery.Infrastructure.Commands.Decorated;

[Decorator]
public class UnitOfWorkDecoratedAddBeerSaleHandler : ICommandHandler<AddBeerSale>
{
    private readonly ICommandHandler<AddBeerSale> _commandHandler;
    private readonly BreweryUnitOfWork _breweryUnitOfWork;

    public UnitOfWorkDecoratedAddBeerSaleHandler(ICommandHandler<AddBeerSale> commandHandler,
        BreweryUnitOfWork breweryUnitOfWork)
    {
        _commandHandler = commandHandler;
        _breweryUnitOfWork = breweryUnitOfWork;
    }

    public async Task HandleAsync(AddBeerSale command)
    {
        await _breweryUnitOfWork.ExecuteAsync(() => _commandHandler.HandleAsync(command));
    }
}