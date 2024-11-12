using Brewery.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Infrastructure.Queries;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return (Task<TResult>)handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>,TResult>.QueryAsync))
            ?.Invoke(handler, new[] { query });
    }
}