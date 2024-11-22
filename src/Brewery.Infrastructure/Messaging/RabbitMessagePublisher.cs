using System.Text;
using System.Text.Json.Serialization;
using Brewery.Abstractions.Auth;
using Brewery.Abstractions.Messaging;
using Humanizer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Brewery.Infrastructure.Messaging;

public class RabbitMessagePublisher : IMessagePublisher
{
    private readonly RabbitMqOptions _options;
    private readonly ConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMessagePublisher> _logger;

    public RabbitMessagePublisher(RabbitMqOptions options,
        ILogger<RabbitMessagePublisher> logger)
    {
        _options = options;
        _connectionFactory = new ConnectionFactory { HostName = _options.HostName };
        _logger = logger;
    }

    public async Task PublishAsync<TMessage>(TMessage message, string exchange) where TMessage : class, IMessage
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        var correlationId = Guid.NewGuid().ToString();
        var callbackQueue = "brewery_id_service_callback_queue";
        
        var routingKey = message.GetType().Name.Underscore();
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        var properties = new BasicProperties();
        properties.CorrelationId = correlationId;
        properties.ReplyTo = callbackQueue;
        
        await channel.ExchangeDeclareAsync(exchange, ExchangeType.Direct, durable: true, autoDelete: false);
        
        await channel.BasicPublishAsync(
            exchange, 
            routingKey, 
            false, 
            properties, 
            body);
    }

    public async Task<TResult> PublishAsync<TMessage, TResult>(TMessage message, string exchange)
        where TMessage : class, IMessage where TResult : JsonWebToken
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        var correlationId = Guid.NewGuid().ToString();
        var callbackQueue = "brewery_id_service_callback_queue";
        
        var routingKey = message.GetType().Name.Underscore();
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        var properties = new BasicProperties();
        properties.CorrelationId = correlationId;
        properties.ReplyTo = callbackQueue;
        
        await channel.ExchangeDeclareAsync(exchange, ExchangeType.Direct, durable: true, autoDelete: false);
        
        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: properties,
            body: body);

        var tcs = new TaskCompletionSource<TResult>();
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            _logger.LogInformation("Start consuming on message received");
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                var responseJson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var responseMessage = JsonConvert.DeserializeObject<TResult>(responseJson);

                tcs.SetResult(responseMessage);

                channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, false);
            }

            return Task.CompletedTask;
        };
        
        await channel.BasicConsumeAsync(queue: callbackQueue, autoAck: false, consumer: consumer);

        return await tcs.Task;
    }
}