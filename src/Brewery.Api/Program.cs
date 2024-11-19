using Brewery.Api;
using Brewery.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Primitives;
using Results = Microsoft.AspNetCore.Http.Results;

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