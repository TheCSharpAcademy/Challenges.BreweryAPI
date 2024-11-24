using System.Security.Claims;

namespace Brewery.Abstractions.Contexts;

public interface IIdentityContext
{
    bool IsAuthenticated { get; }
    Guid Id { get; }
    string Role { get; }
    Dictionary<string, IEnumerable<string>> Claims { get; }
}
