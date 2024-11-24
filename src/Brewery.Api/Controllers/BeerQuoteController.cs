using Brewery.Abstractions.Commands;
using Brewery.Abstractions.Queries;
using Brewery.Application.Commands;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

public class BeerQuoteController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public BeerQuoteController(ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{beerQuoteId}")]
    public async Task<ActionResult<BeerQuoteDto>> Get(Guid beerQuoteId)
        => Ok(await _queryDispatcher.QueryAsync(new GetBeerQuote(beerQuoteId)));

    [HttpPost]
    public async Task<ActionResult> Post(RequestQuote command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new { beerQuoteId = command.RequestQuoteId }, null);
    }
}