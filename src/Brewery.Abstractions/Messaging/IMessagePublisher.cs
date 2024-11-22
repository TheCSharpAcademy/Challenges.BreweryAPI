using Brewery.Abstractions.Auth;

namespace Brewery.Abstractions.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message, string exchange)
        where TMessage : class, IMessage;
    
    Task<TResult> PublishAsync<TMessage, TResult>(TMessage message, string exchange)
        where TMessage : class, IMessage where TResult : JsonWebToken;
}