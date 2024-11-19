namespace Brewery.Abstractions.Contexts;

public interface IContext
{
    string RequestId { get; }
    string TraceId { get; }
    IIdentityContext IdentityContext { get; }
}