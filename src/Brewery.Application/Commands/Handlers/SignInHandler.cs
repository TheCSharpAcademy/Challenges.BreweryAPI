using Brewery.Abstractions.Auth;
using Brewery.Abstractions.Commands;
using Brewery.Abstractions.Messaging;

namespace Brewery.Application.Commands.Handlers;

public class SignInHandler : ICommandHandler<SignIn, JsonWebToken>
{
    private readonly IMessagePublisher _messagePublisher;
    public SignInHandler(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    public async Task<JsonWebToken> HandleAsync(SignIn command)
    {
        var jwt = await _messagePublisher
            .PublishAsync<SignIn, JsonWebToken>(command, "brewery_id_service_exchange");
        
        // var jwt = await _messagePublisher
        //     .PublishAsync<SignIn>(command, "brewery_id_service_exchange");

        return jwt;
    }
}