using Brewery.Domain.Entities;

namespace Brewery.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetUserById(Guid id);
    Task<User> GetUserByEmail(string email);
}