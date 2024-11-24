using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.UnitOfWork;

public interface IBreweryUnitOfWork<TDbContext> where TDbContext : DbContext
{
    
}