using Brewery.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider
            .GetService<ICommandHandler<TCommand>>();
        
        await handler.HandleAsync(command);
    }
}