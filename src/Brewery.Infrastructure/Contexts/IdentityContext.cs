using System.Security.Claims;
using Brewery.Abstractions.Contexts;

namespace Brewery.Infrastructure.Contexts;

public class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string Role { get; }
    public Dictionary<string, IEnumerable<string>> Claims { get; }
    
    public IdentityContext(ClaimsPrincipal principal)
    {
        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
        Role = IsAuthenticated ? principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value : string.Empty;
        Claims = IsAuthenticated
            ? principal.Claims
                .GroupBy(c => c.Type)
                .ToDictionary(
                    k => k.Key,
                    v => v.Select(c => c.Value))
            : new Dictionary<string, IEnumerable<string>>();
    }
    
}