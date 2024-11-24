using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Brewery.Abstractions.Auth;
using Microsoft.IdentityModel.Tokens;

namespace Brewery.Infrastructure.Auth;

public class AuthManager : IAuthManager
{
    private readonly static Dictionary<string, IEnumerable<string>> EmptyClaims = new();
    private readonly AuthOptions _options;
    private readonly string _issuer;
    private readonly SigningCredentials _signingCredentials;

    public AuthManager(AuthOptions options)
    {
        _options = options;
        _issuer = options.Issuer;
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)),
            SecurityAlgorithms.HmacSha256);
    }
    public JsonWebToken GenerateToken(string userId, string role, 
        string audience = null, IDictionary<string, IEnumerable<string>> claims = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id claim (subject) cannot be empty", nameof(userId));
        }

        var now = DateTime.UtcNow;
        var jwtClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
        };

        if (!string.IsNullOrWhiteSpace(role))
        {
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (!string.IsNullOrWhiteSpace(audience))
        {
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
        }

        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var (claim, values) in claims)
            {
                customClaims.AddRange(values.Select(v => new Claim(claim, v)));
            }

            jwtClaims.AddRange(customClaims);
        }

        var expires = now.Add(_options.Expiry);
        var jwt = new JwtSecurityToken(
            _issuer, 
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            RefreshToken = string.Empty,
            Expires = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
            Id = userId,
            Role = role ?? string.Empty,
            Claims = claims ?? EmptyClaims,
        };
    }
}