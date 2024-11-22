using Brewery.Abstractions.Auth;
using Brewery.Abstractions.Commands;
using Brewery.Abstractions.Contexts;
using Brewery.Abstractions.Queries;
using Brewery.Application.Commands;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

public class AccountController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandHandler<SignIn, JsonWebToken> _signInCommandHandler;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IContext _context;

    public AccountController(ICommandDispatcher commandDispatcher,
        ICommandHandler<SignIn, JsonWebToken> signInCommandHandler,
        IQueryDispatcher queryDispatcher,
        IContext context)
    {
        _commandDispatcher = commandDispatcher;
        _signInCommandHandler = signInCommandHandler;
        _queryDispatcher = queryDispatcher;
        _context = context;
    }

    [HttpGet]
    //[Authorize]
    public async Task<ActionResult<AccountDto>> GetUser()
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetUser(_context.IdentityContext.Id)));

    [HttpPost("signUp")]
    public async Task<ActionResult> SignIn(CreateAccount command)
    {
        await _commandDispatcher.DispatchAsync(command with { Id = Guid.NewGuid() });
        return NoContent();
    }

    [HttpPost("signIn")]
    public async Task<ActionResult<JsonWebToken>> SignIn(SignIn command)
        => OkOrNotFound(await _signInCommandHandler.HandleAsync(command));

    // [HttpPost("signIn")]
    // public async Task<ActionResult> SignIn(SignIn command)
    // {
    //     await _commandDispatcher.DispatchAsync(command);
    //     return Ok();
    // }

}