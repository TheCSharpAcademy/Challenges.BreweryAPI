using Brewery.Api;
using Brewery.Infrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var assemblies = AssemblyLoader.GetAssemblies();
        builder.Services.AddInfrastructure(assemblies);

        var app = builder.Build();
        app.UseInfrastructure();

        app.Run(); 
    }
}