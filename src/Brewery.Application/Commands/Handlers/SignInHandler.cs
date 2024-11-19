using Brewery.Abstractions.Auth;
using Brewery.Abstractions.Commands;
using Brewery.Domain.Entities;
using Brewery.Domain.Exceptions;
using Brewery.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Brewery.Application.Commands.Handlers;

public class SignInHandler : ICommandHandler<SignIn, JsonWebToken>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IAuthManager _authManager;

    public SignInHandler(IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IAuthManager authManager)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _authManager = authManager;
    }

    public async Task<JsonWebToken> HandleAsync(SignIn command)
    {
        var user = await _userRepository.GetUserByEmail(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }
        
        var isPasswordVerified = _passwordHasher.
            VerifyHashedPassword(default, user.Password, command.Password)
            == PasswordVerificationResult.Success;
        if (isPasswordVerified is false)
        {
            throw new InvalidCredentialsException();
        }
        
        var jwt = _authManager.GenerateToken(user.Id.ToString(), user.Role, audience: null, user.Claims);

        return jwt;
    }
}