using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Brewery.Domain.ValueObjects;

namespace Brewery.Application.Commands.Handlers;

public class RequestQuoteHandler : ICommandHandler<RequestQuote>
{
    private readonly HashSet<Guid> _beerIds = new();
    private readonly IBeerSaleRepository _beerSaleRepository;
    private readonly IWholesalerRepository _wholesalerRepository;
    private readonly IBeerQuoteRepository _beerQuoteRepository;

    public RequestQuoteHandler(IWholesalerRepository wholesalerRepository,
        IBeerSaleRepository beerSaleRepository,
        IBeerQuoteRepository beerQuoteRepository)
    {
        _wholesalerRepository = wholesalerRepository;
        _beerSaleRepository = beerSaleRepository;
        _beerQuoteRepository = beerQuoteRepository;
    }

    public async Task HandleAsync(RequestQuote command)
    {
        var wholesaler = await _wholesalerRepository.GetWholesaler(command.WholesalerId);
        if (wholesaler is null) throw new WholesalerNotFoundException(command.WholesalerId);

        CheckForDuplicates(command);

        var beerQuote = BeerQuote.Create(command.RequestQuoteId);
        foreach (var enquiry in command.BeersEnquiry)
        {
            var beerSales = await _beerSaleRepository.BrowseByBeerId(enquiry.BeerId);
            var beerSale = beerSales?.SingleOrDefault(b => b.WholesalerId == wholesaler.Id);
            if (beerSale is null)
            {
                throw new WholesalerBeerSaleNotFoundException(wholesaler.Id, enquiry.BeerId);
            }
            
            if (enquiry.RequiredQuantity <= 0)
            {
                throw new OrderCannotBeEmptyException(enquiry.BeerId);
            }

            if (beerSale.Quantity < enquiry.RequiredQuantity)
            {
                throw new NotEnoughBeerForRequestException(beerSale.BeerId, enquiry.RequiredQuantity);
            }
            
            var beerOrder = new BeerOrder(beerSale.BeerId, enquiry.RequiredQuantity, beerSale.UnitPrice);
            beerQuote.AddBeerOrder(beerOrder);
            beerSale.SellBeer(beerOrder.Quantity);
        }
        
        beerQuote.CalculateDiscount();
        beerQuote.CalculateTotal();
        await _beerQuoteRepository.AddAsync(beerQuote);
    }

    private void CheckForDuplicates(RequestQuote command)
    {
        _beerIds.Clear();
        foreach (var beerEnquiry in command.BeersEnquiry)
        {
            var beerId = beerEnquiry.BeerId;
            var exists = _beerIds.TryGetValue(beerId, out _);
            if (exists)
            {
                throw new DuplicatesInRequestQuoteException();
            }
                
            _beerIds.Add(beerId);

        }
    }
}