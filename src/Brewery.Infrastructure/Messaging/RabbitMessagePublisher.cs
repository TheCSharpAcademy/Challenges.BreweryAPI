using System.Text;
using Brewery.Abstractions.Auth;
using Brewery.Abstractions.Messaging;
using Brewery.Infrastructure.Messaging.RabbitMq;
using Humanizer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Brewery.Infrastructure.Messaging;

public class RabbitMessagePublisher : IMessagePublisher
{
    private readonly RabbitMqOptions _options;
    private IConnectionManager _connectionManager;
    private readonly ILogger<RabbitMessagePublisher> _logger;

    public RabbitMessagePublisher(RabbitMqOptions options,
        ILogger<RabbitMessagePublisher> logger,
        IConnectionManager connectionManager)
    {
        _options = options;
        _connectionManager = connectionManager;
        _logger = logger;
    }

    public async Task PublishAsync<TMessage>(TMessage message, string exchange) where TMessage : class, IMessage
    {
        var channel = await _connectionManager.CreateChannel();
        
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
        var channel = await _connectionManager.CreateChannel();
        
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
        consumer.ReceivedAsync += async (model, ea) =>
        {
            _logger.LogInformation("Start consuming on message received");
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                var responseJson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var responseMessage = JsonConvert.DeserializeObject<TResult>(responseJson);

                tcs.SetResult(responseMessage);

                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, false);
            }

        };

        await channel.BasicConsumeAsync(queue: callbackQueue, autoAck: false, consumer: consumer);
        var result = await tcs.Task;
        
        await channel.DisposeAsync();

        return result;
        
    }
}