using Brewery.Abstractions.Messaging;
using RabbitMQ.Client;

namespace Brewery.Infrastructure.Messaging.RabbitMq;

public class ConnectionManager : IConnectionManager
{
    private readonly IConnectionFactory _factory;
    private readonly RabbitMqOptions _options;
    private readonly Lazy<Task<IConnection>> _lazyConnection;

    public ConnectionManager(RabbitMqOptions options)
    {
        _options = options;
        _factory = new ConnectionFactory { HostName = _options.HostName };
        _lazyConnection = new Lazy<Task<IConnection>>(() => _factory.CreateConnectionAsync());
    }
    
    public async Task<IChannel> CreateChannel()
    {
        var connection = await _lazyConnection.Value;
        var channel = await connection.CreateChannelAsync();
        
        return channel;
    }
}