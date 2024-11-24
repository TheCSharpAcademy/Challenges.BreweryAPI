using System.Net;
using System.Text;
using Brewery.Application.Commands;
using Brewery.Tests.Shared.Factories;
using Brewery.Tests.Shared.Fixtures;
using Newtonsoft.Json;
using Shouldly;

namespace Brewery.Tests.EndToEnd.Sync;

[Collection($"BreweryDbFixture")]
public class AddBrewerTests : IClassFixture<BreweryAppFactory>, IClassFixture<BreweryDbFixture>
{
    private async Task<HttpResponseMessage> Act() => await _client.PostAsync("brewer", GetPayload());

    [Fact]
    public async Task add_brewer_endpoint_should_return_status_code_created_at_action()
    {
        await _breweryDbFixture.SeedDatabaseAsync(_breweryId);
    
        var response = await Act();
    
        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    private readonly HttpClient _client;
    private readonly BreweryDbFixture _breweryDbFixture;
    private readonly Guid _breweryId;
    
    public AddBrewerTests(BreweryAppFactory factory, BreweryDbFixture breweryDbFixture)
    {
        factory.Server.AllowSynchronousIO = true;
        _client = factory.CreateClient();
        _breweryDbFixture = breweryDbFixture;
        _breweryId = Guid.NewGuid();
    }

    private StringContent GetPayload()
    {
        var json = JsonConvert.SerializeObject(new AddBrewer("brewer 1", _breweryId));
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}