using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Persistence;

namespace ToDoListApp.IntegrationTests.Controllers;

public class ProfilesApiTests : IClassFixture<InMemoryToDoWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProfilesApiTests(InMemoryToDoWebApplicationFactory factory)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

        _client = factory.CreateClient();

        var token = TestJwtHelper.GenerateJwt();
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ToDoContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    [Fact]
    public async Task CreateProfile_ReturnsOk_AndProfileDto()
    {
        var response = await _client.PostAsJsonAsync("Profiles", new { });
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var profile = await response.Content.ReadFromJsonAsync<ProfileDto>();
        profile.Should().NotBeNull();
        profile.Id.Should().BeGreaterThan(0);
        profile.UserId.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task CreatingProfileTwice_ForSameUser_ReturnsConflict()
    {
        var firstResp = await _client.PostAsJsonAsync("Profiles", new { });
        firstResp.EnsureSuccessStatusCode();

        var secondResp = await _client.PostAsJsonAsync("Profiles", new { });
        secondResp.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
    
    [Fact]
    public async Task Unauthorized_Requests_Return_401()
    {
        _client.DefaultRequestHeaders.Authorization = null;
        var response = await _client.PostAsync("Profiles", null);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}