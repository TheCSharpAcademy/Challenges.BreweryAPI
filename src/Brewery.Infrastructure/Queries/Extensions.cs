using System.Reflection;
using Brewery.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(a => a.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}