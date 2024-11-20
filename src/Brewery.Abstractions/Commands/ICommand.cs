using Brewery.Abstractions.Messaging;

namespace Brewery.Abstractions.Commands;

public interface ICommand : IMessage
{
    
}

public interface ICommand<TResult> : ICommand;