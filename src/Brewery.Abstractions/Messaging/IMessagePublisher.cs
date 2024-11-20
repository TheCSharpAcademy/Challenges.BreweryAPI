namespace Brewery.Abstractions.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message, string exchange)
        where TMessage : class, IMessage;
}