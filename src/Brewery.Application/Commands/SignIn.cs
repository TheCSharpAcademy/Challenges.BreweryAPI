using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record SignIn(string Email, string Password) : ICommand;