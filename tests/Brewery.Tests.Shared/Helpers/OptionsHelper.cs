using Microsoft.Extensions.Configuration;

namespace Brewery.Tests.Shared.Helpers;

public class OptionsHelper
{
    private const string AppSettings = "appsettings.test.json";
    public static TOptions GetOptions<TOptions>(string sectionName) where TOptions : new()
    {
        TOptions options = new TOptions();
        var configuration = GetConfiguration();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);
        
        return options;
    }

    private static IConfigurationRoot GetConfiguration()
        => new ConfigurationBuilder()
            .AddJsonFile(AppSettings)
            .AddEnvironmentVariables()
            .Build();
}