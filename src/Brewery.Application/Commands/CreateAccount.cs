using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record CreateAccount(Guid Id, string Email, string Password, string Role,
    Dictionary<string, IEnumerable<string>> Claims) : ICommand;