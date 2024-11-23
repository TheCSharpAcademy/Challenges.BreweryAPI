using Brewery.Abstractions.Messaging;
using RabbitMQ.Client;

namespace Brewery.Infrastructure.Messaging.RabbitMq;

public class ConnectionManager : IConnectionManager
{
    private readonly IConnectionFactory _factory;
    private readonly RabbitMqOptions _options;
    
    public ConnectionManager(RabbitMqOptions options)
    {
        _options = options;
        _factory = new ConnectionFactory { HostName = _options.HostName };
    }
    
    public async Task<IChannel> CreateChannel()
    {
        var connection = await _factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        return channel;
    }
}