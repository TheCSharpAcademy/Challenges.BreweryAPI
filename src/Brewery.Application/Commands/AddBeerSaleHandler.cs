using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Repositories;

namespace Brewery.Application.Commands;

public class AddBeerSaleHandler : ICommandHandler<AddBeerSale>
{
    private readonly IWholesalerRepository _wholesalerRepository;
    private readonly ISaleRepository _saleRepository;

    public AddBeerSaleHandler(IWholesalerRepository wholesalerRepository,
        ISaleRepository saleRepository)
    {
        _wholesalerRepository = wholesalerRepository;
        _saleRepository = saleRepository;
    }

    public async Task HandleAsync(AddBeerSale command)
    {
        var wholesaler = await _wholesalerRepository.GetWholesaler(command.WholesalerId);
        if (wholesaler is null)
        {
            throw new WholesalerNotFoundException(command.WholesalerId);
        }
        
        var beerSale = await _saleRepository.GetSaleByBeerId(command.BeerId);
        if (beerSale is null)
        {
            throw new BeerSaleNotFoundException(command.BeerId);
        }

        if (beerSale.Quantity < command.Quantity)
        {
            throw new ExcessiveBeerSaleOrderQuantityException(command.Quantity);
        }
        
        beerSale.TakeBeer(command.Quantity);
        wholesaler.AddBeerSale(beerSale);
        
        await _wholesalerRepository
    }
}