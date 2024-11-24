using Brewery.Abstractions.Commands;
using Brewery.Domain.ValueObjects;

namespace Brewery.Application.Commands;

public record RequestQuote(Guid WholesalerId, IEnumerable<BeerEnquiry> BeersEnquiry) : ICommand
{
    public Guid RequestQuoteId { get; } = Guid.NewGuid();
}