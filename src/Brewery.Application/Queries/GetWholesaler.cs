using Brewery.Abstractions.Queries;
using Brewery.Application.DTO;
using Brewery.Domain.Entities;

namespace Brewery.Application.Queries;

public record GetWholesaler(Guid WholesalerId) : IQuery<WholesalerDto>;