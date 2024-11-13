using Brewery.Abstractions.Commands;
using Brewery.Abstractions.Queries;
using Brewery.Application.Commands;
using Brewery.Application.Commands.Handlers;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

public class BeerController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public BeerController(ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{beerId:guid}")]
    public async Task<ActionResult<BeerDto>> GetBeer(Guid beerId)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetBeer(beerId)));
    
    [HttpPost]
    public async Task<ActionResult> AddBeer(AddBeer command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetBeer), new { beerId = command.Id }, null);
    }
    
    [HttpPut("{beerId:guid}")]
    public async Task<ActionResult> UpdateBeer(UpdateBeer command, Guid beerId)
    {
        await _commandDispatcher.DispatchAsync(command with { Id = beerId });
        return NoContent();
    }

    [HttpDelete("{beerId:guid}")]
    public async Task<ActionResult> DeleteBeer(Guid beerId)
    {
        await _commandDispatcher.DispatchAsync(new DeleteBeer(beerId));
        return NoContent();
    }
}