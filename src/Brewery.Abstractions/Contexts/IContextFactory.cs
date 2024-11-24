namespace Brewery.Abstractions.Contexts;

public interface IContextFactory
{
    IContext Create();
}