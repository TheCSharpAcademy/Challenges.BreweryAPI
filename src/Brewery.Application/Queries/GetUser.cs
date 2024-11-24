using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;

namespace Brewery.Application.Queries;

public record GetUser(Guid UserId) : IQuery<AccountDto>;