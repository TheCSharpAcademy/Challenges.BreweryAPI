using Brewery.Abstractions.Exceptions;

namespace Brewery.Application.Exceptions;

public class BreweryNotFoundException : BreweryException
{
        public Guid BreweryId { get; }
        public BreweryNotFoundException(Guid breweryId)
            : base($"Brewery with id '{breweryId}' was not found.")
        {
            BreweryId = breweryId;
        }
}