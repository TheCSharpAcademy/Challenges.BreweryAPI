using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Brewery.Infrastructure.Middleware;

public class EndpointsInfoMiddleware : IMiddleware
{
    private readonly EndpointDataSource _endpointDataSource;

    public EndpointsInfoMiddleware(EndpointDataSource endpointDataSource)
    {
        _endpointDataSource = endpointDataSource;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoints = _endpointDataSource.Endpoints
            .Select(e =>
            {
                var routeEndpoint = e as RouteEndpoint;

                return new
                {
                    Endpoint = e.DisplayName,
                    Methods = string.Join(", ", e.Metadata.OfType<HttpMethodMetadata>()
                        .Select(m => string.Join(", ", m.HttpMethods))),
                    Route = routeEndpoint?.RoutePattern?.RawText,
                };
            })
            .ToArray();
        
        await context.Response.WriteAsJsonAsync(endpoints);
    }
}