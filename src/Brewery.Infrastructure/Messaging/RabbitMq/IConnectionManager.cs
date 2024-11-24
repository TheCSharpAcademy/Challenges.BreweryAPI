using RabbitMQ.Client;

namespace Brewery.Infrastructure.Messaging.RabbitMq;

public interface IConnectionManager
{
    Task<IChannel> CreateChannel();
}