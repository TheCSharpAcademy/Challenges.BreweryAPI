using System.Security.Cryptography;
using Brewery.Abstractions.Commands;
using Brewery.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

public class BeerStockController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;

    public BeerStockController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{brewerId:guid}")]
    public async Task<ActionResult> Post(AddBeerStock addBeerStock, Guid brewerId)
    {
        await _commandDispatcher.DispatchAsync(addBeerStock with { BrewerId = brewerId });
        return NoContent();
    }
}