using Brewery.Abstractions.Messaging;
using Brewery.Infrastructure.Messaging.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.Messaging;

public static class Extensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        var options = services.GetOptions<RabbitMqOptions>("rabbitmq");
        services.AddSingleton(options);
        services.AddSingleton<IMessagePublisher, RabbitMessagePublisher>();
        services.AddSingleton<IConnectionManager, ConnectionManager>();
        
        return services;
    }
}