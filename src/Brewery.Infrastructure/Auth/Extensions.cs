using System.Text;
using Brewery.Abstractions.Auth;
using Brewery.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Brewery.Infrastructure.Auth;

public static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        var authOptions = services.GetOptions<AuthOptions>("auth");
        services.AddSingleton<IAuthManager, AuthManager>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authOptions.ValidIssuer,
            ValidateIssuer = authOptions.ValidateIssuer,
            ValidateAudience = authOptions.ValidateAudience,
            ValidateLifetime = authOptions.ValidateLifetime,
            ClockSkew = TimeSpan.Zero   

        };
        if (string.IsNullOrWhiteSpace(authOptions.IssuerSigningKey))
        {
            throw new ArgumentException("Missing issuer signing key.", nameof(authOptions.IssuerSigningKey));
        }
        
        var rawKey = Encoding.UTF8.GetBytes(authOptions.IssuerSigningKey);
        tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);
        
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = tokenValidationParameters;
        });

        services.AddSingleton(authOptions);
        services.AddSingleton(tokenValidationParameters);
        
        services.AddAuthorization();
        
        return services;
    }
}