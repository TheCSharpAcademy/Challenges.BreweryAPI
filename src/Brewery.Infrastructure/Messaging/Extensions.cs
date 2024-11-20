using Brewery.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.Messaging;

public static class Extensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        var options = services.GetOptions<RabbitMqOptions>("rabbitmq");
        services.AddSingleton(options);
        services.AddSingleton<IMessagePublisher, RabbitMessagePublisher>();
        
        return services;
    }
}