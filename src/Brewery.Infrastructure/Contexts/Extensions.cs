using Brewery.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.Contexts;

public static class Extensions
{
    public static IServiceCollection AddContexts(this IServiceCollection services)
    {
        services.AddSingleton<IContextFactory, ContextFactory>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IContext>(sp => sp.GetRequiredService<IContextFactory>().Create());

        return services;
    }
}