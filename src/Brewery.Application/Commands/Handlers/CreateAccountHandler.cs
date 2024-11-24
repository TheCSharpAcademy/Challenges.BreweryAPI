using Brewery.Abstractions.Commands;
using Brewery.Application.Exceptions;
using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Brewery.Application.Commands.Handlers;

public class CreateAccountHandler : ICommandHandler<CreateAccount>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public CreateAccountHandler(IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(CreateAccount command)
    {
        var user = await _userRepository.GetUserByEmail(command.Email);
        if (user is not null)
        {
            throw new EmailInUseException(command.Email);
        }
        
        var hashedPassword = _passwordHasher.HashPassword(default, command.Password);
        user = new User(
            command.Id, 
            command.Email, 
            hashedPassword, 
            command.Role is not null ? command.Role : "user",
            command.Claims is not null ? command.Claims : new Dictionary<string, IEnumerable<string>>(),
            DateTime.UtcNow);
        
        await _userRepository.AddAsync(user);
    }
}