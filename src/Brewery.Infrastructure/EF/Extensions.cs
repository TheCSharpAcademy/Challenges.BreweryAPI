using Brewery.Abstractions.Postgres;
using Brewery.Domain.Repositories;
using Brewery.Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.EF;

public static class Extensions
{
    internal static IServiceCollection AddEF(this IServiceCollection services)
    {
        services.AddScoped<IBeerRepository, BeerRepository>();
        services.AddScoped<IBrewerRepository, BrewerRepository>();
        services.AddScoped<IBreweryRepository, BreweryRepository>();
        services.AddScoped<IWholesalerRepository, WholesalerRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IBeerStockRepository, BeerStockRepository>();
        
        var postgresOptions = services.GetOptions<PostgresOptions>("postgres");
        services.AddDbContext<BreweryDbContext>(options =>
        {
            options.UseNpgsql(postgresOptions.ConnectionString);
        });
        
        return services;
    }
}