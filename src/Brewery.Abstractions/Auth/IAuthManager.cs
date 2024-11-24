namespace Brewery.Abstractions.Auth;

public interface IAuthManager
{
    JsonWebToken GenerateToken(string userId, string role, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null);
}