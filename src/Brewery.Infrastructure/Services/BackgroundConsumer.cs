using Microsoft.Extensions.Hosting;

namespace Brewery.Infrastructure.Services;

public class BackgroundConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public BackgroundConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        throw new NotImplementedException();
    }
}