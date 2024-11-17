using System.Net;
using System.Text;
using Brewery.Application.Commands;
using Brewery.Domain.Entities;
using Brewery.Tests.Shared.Factories;
using Brewery.Tests.Shared.Fixtures;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shouldly;

namespace Brewery.Tests.EndToEnd.Sync;

[Collection($"BreweryDbFixture")]
public class AddBeerTests : IClassFixture<BreweryAppFactory>, IClassFixture<BreweryDbFixture>
{
    private Task<HttpResponseMessage> Act() => _httpClient.PostAsync("Beer", GetPayload(_command));

    [Fact]
    public async Task add_beer_endpoint_should_return_status_code_created_at_action()
    {
        await SeedDatabase();

        var response = await Act();

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task add_beer_endpoint_should_return_location_header_with_correct_beer_id()
    {
        await SeedDatabase();

        var response = await Act();
        
        var locationHeader = response.Headers
            .FirstOrDefault(h => h.Key == "Location")
            .Value.First();
        locationHeader.ShouldNotBeNull();
        locationHeader.ShouldBe($"http://localhost/Beer/{_command.Id}");
    }

    [Fact]
    public async Task add_beer_endpoint_should_add_beer_entity_with_given_id_to_database()
    {
        await SeedDatabase();
        
        var response = await Act();

        var beerFromDb = await _breweryDbFixture.BreweryDbContext.Beers.SingleOrDefaultAsync(b => b.Id == _beerId);
        beerFromDb.ShouldNotBeNull();
        beerFromDb.Id.ShouldBe(_command.Id);
    }

    private readonly HttpClient _httpClient;
    private readonly BreweryDbFixture _breweryDbFixture;
    private readonly Guid _beerId;
    private readonly Guid _brewerId;
    private readonly AddBeer _command;

    public AddBeerTests(BreweryAppFactory factory, BreweryDbFixture breweryDbFixture)
    {
        factory.Server.AllowSynchronousIO = true;
        _httpClient = factory.CreateClient();
        _breweryDbFixture = breweryDbFixture;
        _beerId = Guid.NewGuid();
        _brewerId = Guid.NewGuid();
        _command = new AddBeer(_brewerId, "beer 1") { Id = _beerId };
    }       

    private StringContent GetPayload(AddBeer command)
    {
        var json = JsonConvert.SerializeObject(command);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private async Task SeedDatabase()
    {
        var brewer = Brewer.Create(_brewerId, "brewer 1");
        await _breweryDbFixture.BreweryDbContext.Brewers.AddAsync(brewer);
        await _breweryDbFixture.BreweryDbContext.SaveChangesAsync();
    }
}