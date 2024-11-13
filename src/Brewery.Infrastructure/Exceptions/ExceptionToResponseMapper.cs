using System.Net;
using Brewery.Abstractions.Exceptions;
using Humanizer;

namespace Brewery.Infrastructure.Exceptions;

public class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            BreweryException breweryException => new ExceptionResponse(
                new Error(GetErrorCode(exception), breweryException.Message),
                HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(
                new Error("error", "There was an error."),
                HttpStatusCode.InternalServerError)
        };
    
    private static string GetErrorCode(Exception exception)
        => exception.GetType().Name.Underscore().Replace("_exception", string.Empty);
}