using Brewery.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;

namespace Brewery.Infrastructure.Contexts;

public class Context : IContext
{
    public string RequestId { get; } = $"{Guid.NewGuid():N}";
    public string TraceId { get; }
    public IIdentityContext IdentityContext { get; }

    internal Context()
    {
    }

    public Context(IHttpContextAccessor httpContextAccessor) :
        this(httpContextAccessor.HttpContext.TraceIdentifier,
            new IdentityContext(httpContextAccessor.HttpContext.User))
    {

    }

    public Context(string traceId, IIdentityContext identityContext)
    {
        TraceId = traceId;
        IdentityContext = identityContext;
    }

    public static Context Empty() => new Context();
}