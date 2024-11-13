using System.Net;
using Brewery.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Brewery.Infrastructure.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    private readonly IExceptionToResponseMapper _exceptionToResponseMapper;

    public ExceptionMiddleware(IExceptionToResponseMapper exceptionToResponseMapper)
    {
        _exceptionToResponseMapper = exceptionToResponseMapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var response = _exceptionToResponseMapper.Map(ex);
            context.Response.StatusCode = (int)response.HttpStatusCode;
            await context.Response.WriteAsJsonAsync(response.Error);
        }
    }
}