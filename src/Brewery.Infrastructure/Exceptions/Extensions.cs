using Brewery.Abstractions.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.Exceptions;

public static class Extensions
{
    public static IServiceCollection AddExceptionHanding(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddScoped<ExceptionMiddleware>();
        
        return services;
    }

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        
        return app;
    }
}