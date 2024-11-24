namespace Brewery.Infrastructure.EF.UnitOfWork;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}