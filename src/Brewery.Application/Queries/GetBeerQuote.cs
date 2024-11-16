using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;

namespace Brewery.Application.Queries;

public record GetBeerQuote(Guid BeerQuoteId) : IQuery<BeerQuoteDto>;