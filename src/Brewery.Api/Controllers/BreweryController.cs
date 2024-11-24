using Brewery.Abstractions.Commands;
using Brewery.Abstractions.Queries;
using Brewery.Application.Commands;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

public class BreweryController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public BreweryController(ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("{breweryId:guid}")]
    public async Task<ActionResult<BreweryDto>> Get(Guid breweryId)
    {
        var brewery = await _queryDispatcher.QueryAsync(new GetBrewery(breweryId));
        return OkOrNotFound(brewery);
    }
    
    [Authorize]
    [HttpGet("{breweryId:guid}/beers")]
    public async Task<ActionResult<IEnumerable<BeerDto>>> Browse(Guid breweryId)
    {
        var beersDto = await _queryDispatcher
            .QueryAsync(new BrowseBeersByBrewery(breweryId));
        return Ok(beersDto);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Post(AddBrewery command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new { breweryId = command.Id }, null);
    }
}