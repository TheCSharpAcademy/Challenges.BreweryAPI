using Brewery.Abstractions.Auth;
using Brewery.Abstractions.Commands;
using Brewery.Abstractions.Messaging;
using Brewery.Domain.Entities;
using Brewery.Domain.Exceptions;
using Brewery.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Brewery.Application.Commands.Handlers;

public class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IMessagePublisher _messagePublisher;
    public SignInHandler(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    public async Task HandleAsync(SignIn command)
    {
        await _messagePublisher.PublishAsync(command, "brewery-id-service-exchange");
    }
}