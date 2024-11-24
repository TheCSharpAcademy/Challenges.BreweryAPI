using Brewery.Domain.Entities;
using Brewery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly BreweryDbContext _dbContext;

    public UserRepository(BreweryDbContext dbContext)
    {
        _users = dbContext.Users;
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public Task<User> GetUserById(Guid id)
        => _users.SingleOrDefaultAsync(u => u.Id == id);
    
    public Task<User> GetUserByEmail(string email)
        => _users.SingleOrDefaultAsync(u => u.Email == email);
}