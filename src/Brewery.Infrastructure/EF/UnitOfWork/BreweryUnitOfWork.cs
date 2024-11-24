using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.UnitOfWork;

public class BreweryUnitOfWork : IUnitOfWork
{
    private readonly BreweryDbContext _dbContext;

    public BreweryUnitOfWork(BreweryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}