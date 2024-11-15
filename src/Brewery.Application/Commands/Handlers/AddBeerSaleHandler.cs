using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBeerSaleHandler : ICommandHandler<AddBeerSale>
{
    private readonly IWholesalerRepository _wholesalerRepository;
    private readonly IBeerStockRepository _beerStockRepository;

    public AddBeerSaleHandler(IWholesalerRepository wholesalerRepository,
        IBeerStockRepository beerStockRepository)
    {
        _wholesalerRepository = wholesalerRepository;
        _beerStockRepository = beerStockRepository;
    }

    public async Task HandleAsync(AddBeerSale command)
    {
        var wholesaler = await _wholesalerRepository.GetWholesaler(command.WholesalerId);
        if (wholesaler is null)
        {
            throw new WholesalerNotFoundException(command.WholesalerId);
        }
        
        var beerSale = wholesaler.BeerSales.SingleOrDefault(b => b.Id == command.BeerId);
        if (beerSale is not null)
        {
            throw new BeerSaleAlreadyExistException(command.WholesalerId, command.BeerId);
        }
        
        var beerStock = await _beerStockRepository.GetBeerStock(command.BeerId);
        if (beerStock is null)
        {
            throw new BeerStockNotFoundException(command.BeerId);
        }

        if (beerStock.Quantity < command.Quantity)
        {
            throw new BeerStockNotEnoughToFulfilOrderException(command.BeerId, command.Quantity);
        }
        
        beerSale = BeerSale.Create(command.WholesalerId, command.BeerId, 50);
        beerStock.TakeForBeerSale(beerSale.Quantity);
        wholesaler.AddBeerSale(beerSale);
        
        await _beerStockRepository.UpdateBeerStock(beerStock);
        await _wholesalerRepository.UpdateWholesaler(wholesaler);
    }
}