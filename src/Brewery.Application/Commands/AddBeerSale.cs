using Brewery.Abstractions.Commands;

namespace Brewery.Application.Commands;

public record AddBeerSale(Guid WholesalerId, Guid BeerId, int Quantity) : ICommand;