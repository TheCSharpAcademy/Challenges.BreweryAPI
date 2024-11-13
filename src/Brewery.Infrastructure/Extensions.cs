using System.Reflection;
using Brewery.Infrastructure.Commands;
using Brewery.Infrastructure.EF;
using Brewery.Infrastructure.Exceptions;
using Brewery.Infrastructure.Middleware;
using Brewery.Infrastructure.Queries;
using Brewery.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddExceptionHanding();
        services.AddControllers();
        services.AddHostedService<AppInitializer>();
        services.AddSingleton<EndpointsInfoMiddleware>();
        services.AddEF();
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        
        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseExceptionHandling();
        app.UseRouting();
        app.UseEndpoints(e =>
        {
            e.MapControllers();
            e.MapGet("/", () => "Hello from Brewery Api!");
            e.MapGet("/endpoints", async (HttpContext context, EndpointsInfoMiddleware middleware) =>
            {
                await middleware.InvokeAsync(context, _ => Task.CompletedTask);
            });
        });

        return app;
    }

    public static TOptions GetOptions<TOptions>(this IServiceCollection services, string sectionName)
        where TOptions : class, new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        
        return configuration.GetOptions<TOptions>(sectionName);
    }

    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
        where TOptions : class, new()
    {
        var options = new TOptions();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);
        
        return options;
    }
}