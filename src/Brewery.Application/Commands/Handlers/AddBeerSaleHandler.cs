using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands.Handlers;

public class AddBeerSaleHandler : ICommandHandler<AddBeerSale>
{
    private readonly IWholesalerRepository _wholesalerRepository;
    private readonly IBeerStockRepository _beerStockRepository;
    private readonly IBeerSaleRepository _beerSaleRepository;

    public AddBeerSaleHandler(IWholesalerRepository wholesalerRepository,
        IBeerStockRepository beerStockRepository,
        IBeerSaleRepository beerSaleRepository)
    {
        _wholesalerRepository = wholesalerRepository;
        _beerStockRepository = beerStockRepository;
        _beerSaleRepository = beerSaleRepository;
    }

    public async Task HandleAsync(AddBeerSale command)
    {
        var wholesaler = await _wholesalerRepository.GetWholesaler(command.WholesalerId);
        if (wholesaler is null)
        {
            throw new WholesalerNotFoundException(command.WholesalerId);
        }
        
        var beerStock = await _beerStockRepository.GetBeerStock(command.BeerId);
        if (beerStock is null)
        {
            throw new BeerStockNotFoundException(command.BeerId);
        }
        
        var beerSales = await _beerSaleRepository.BrowseByBeerId(command.BeerId);
        var beerSale = beerSales?.SingleOrDefault(b => b.WholesalerId == wholesaler.Id);
        if (beerSale is not null)
        {
            throw new BeerSaleAlreadyExistException(command.WholesalerId, command.BeerId);
        }
        
        if (beerStock.Quantity < command.Quantity)
        {
            throw new NotEnoughBeerForRequestException(command.BeerId, command.Quantity);
        }
        
        beerSale = BeerSale.Create(Guid.NewGuid(), beerStock.BeerId, 
            wholesaler.Id, command.Quantity, beerStock.UnitPrice);
        
        beerStock.TakeForBeerSale(beerSale.Quantity);
        await _beerStockRepository.UpdateBeerStock(beerStock);
        
        //wholesaler.AddBeerSale(beerSale);
        await _beerSaleRepository.AddAsync(beerSale);
        //await _wholesalerRepository.UpdateWholesaler(wholesaler);
        // unit of work pattern?
    }
}