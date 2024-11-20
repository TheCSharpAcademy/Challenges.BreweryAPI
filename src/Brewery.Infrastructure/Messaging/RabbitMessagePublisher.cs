using System.Text;
using System.Text.Json.Serialization;
using Brewery.Abstractions.Messaging;
using Humanizer;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Brewery.Infrastructure.Messaging;

public class RabbitMessagePublisher : IMessagePublisher
{
    private readonly RabbitMqOptions _options;
    private readonly IModel _channel;

    public RabbitMessagePublisher(RabbitMqOptions options)
    {
        _options = options;
        var connectionFactory = new ConnectionFactory
        {
            HostName = _options.HostName,
        };

        var connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public Task PublishAsync<TMessage>(TMessage message, string exchange) where TMessage : class, IMessage
    {
        var routingKey = message.GetType().Name.Underscore();
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        //var properties = _channel.CreateBasicProperties();
        
        _channel.ExchangeDeclare(exchange, ExchangeType.Direct, durable: true, autoDelete: false);
        
        _channel.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            //basicProperties: properties,
            body: body);
        
        return Task.CompletedTask;
    }
}