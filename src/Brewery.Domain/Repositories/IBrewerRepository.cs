using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface IBrewerRepository
{
    Task AddBrewer(Brewer brewer);
    Task<Brewer> GetBrewer(Guid brewerId);
}