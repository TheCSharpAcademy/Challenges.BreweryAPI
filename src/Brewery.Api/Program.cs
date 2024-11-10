var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapGet("/", () => "Hello from Brewery Api!");

app.Run();
