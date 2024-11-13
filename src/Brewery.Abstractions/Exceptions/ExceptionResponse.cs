using System.Net;

namespace Brewery.Abstractions.Exceptions;

public record ExceptionResponse(Error Error, HttpStatusCode HttpStatusCode);
public record Error(string Code, string Message);