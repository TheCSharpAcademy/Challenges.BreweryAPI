using System.Reflection;
using Brewery.Abstractions.Auth;
using Brewery.Abstractions.Commands;
using Brewery.Application.Commands;
using Brewery.Application.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        services.Scan(a => a.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        //services.AddScoped<ICommandHandler<SignIn, JsonWebToken>, SignInHandler>();
        
        return services;
    }
}