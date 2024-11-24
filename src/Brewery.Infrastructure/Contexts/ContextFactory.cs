using Brewery.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;

namespace Brewery.Infrastructure.Contexts;

public class ContextFactory : IContextFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ContextFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IContext Create()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return Context.Empty();
        }
        
        return new Context(_httpContextAccessor);
    }
}