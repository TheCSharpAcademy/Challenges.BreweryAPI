using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;
using Brewery.Application.Queries;
using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Infrastructure.EF.Queries.Handlers;

public class GetUserHandler : IQueryHandler<GetUser, AccountDto>
{
    private readonly DbSet<User> _users;

    public GetUserHandler(BreweryDbContext dbContext)
    {
        _users = dbContext.Users;
    }
    public async Task<AccountDto> QueryAsync(GetUser query)
    {
        var user = await _users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == query.UserId);

        return user is not null
            ? new AccountDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role,
                Claims = user.Claims,
            }
            : null;
    }
}