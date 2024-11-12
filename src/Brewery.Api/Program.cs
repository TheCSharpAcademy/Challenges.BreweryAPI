using Brewery.Api;
using Brewery.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var assemblies = AssemblyLoader.GetAssemblies();
builder.Services.AddInfrastructure(assemblies);

var app = builder.Build();
app.UseInfrastructure();

app.Run();
