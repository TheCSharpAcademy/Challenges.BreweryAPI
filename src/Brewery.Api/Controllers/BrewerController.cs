using Brewery.Abstractions.Commands;
using Brewery.Abstractions.Queries;
using Brewery.Application.Commands;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

public class BrewerController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    : BaseController
{
    [HttpGet("{brewerId:guid}")]
    public async Task<ActionResult<BrewerDto>> Get(Guid brewerId)
        => Ok(await queryDispatcher.QueryAsync(new GetBrewer(brewerId)));
    
    [HttpPost]
    public async Task<ActionResult> AddBrewer(AddBrewer command)
    {
        await commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new { brewerId = command.Id }, null);
    }
}