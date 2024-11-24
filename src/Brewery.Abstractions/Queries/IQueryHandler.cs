namespace Brewery.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> QueryAsync(TQuery query);
}