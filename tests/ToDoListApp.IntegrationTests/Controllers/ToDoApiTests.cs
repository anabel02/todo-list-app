using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Application.Commands;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Persistence;

namespace ToDoListApp.IntegrationTests.Controllers;

public class ToDoApiTests : IClassFixture<InMemoryToDoWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ToDoApiTests(InMemoryToDoWebApplicationFactory factory)
    {
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
    public async Task GetTodos_ReturnsOkAndEmptyListInitially()
    {
        await _client.PostAsJsonAsync("Profiles", new { });
        var response = await _client.GetAsync("ToDos");
        response.EnsureSuccessStatusCode();

        var todos = await response.Content.ReadFromJsonAsync<List<ToDoDto>>();
        todos.Should().NotBeNull();
        todos.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateTodo_WithoutProfile_ReturnsForbidden()
    {
        var body = new CreateTaskCommandBody("No profile yet");
        var response = await _client.PostAsJsonAsync("ToDos", body);
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateTodo_ReturnsCreatedTodo()
    {
        await _client.PostAsJsonAsync("Profiles", new { });
        var body = new CreateTaskCommandBody("Test Task");
        var response = await _client.PostAsJsonAsync("ToDos", body);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var todo = await response.Content.ReadFromJsonAsync<ToDoDto>();
        todo.Should().NotBeNull();
        todo.Task.Should().Be(body.Task);
        todo.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetById_ReturnsTodo()
    {
        await _client.PostAsJsonAsync("Profiles", new { });
        var body = new CreateTaskCommandBody("GetById test");
        var createResp = await _client.PostAsJsonAsync("ToDos", body);
        var created = await createResp.Content.ReadFromJsonAsync<ToDoDto>();

        var response = await _client.GetAsync($"ToDos/{created!.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var todo = await response.Content.ReadFromJsonAsync<ToDoDto>();
        todo.Should().NotBeNull();
        todo!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task UpdateTodo_UpdatesSuccessfully()
    {
        await _client.PostAsJsonAsync("Profiles", new { });
        var createBody = new CreateTaskCommandBody("Update test");
        var createResp = await _client.PostAsJsonAsync("ToDos", createBody);
        var created = await createResp.Content.ReadFromJsonAsync<ToDoDto>();

        var updateBody = new UpdateTaskBody("Updated title");
        var updateResp = await _client.PutAsJsonAsync($"ToDos/{created!.Id}", updateBody);
        updateResp.StatusCode.Should().Be(HttpStatusCode.OK);

        var updated = await updateResp.Content.ReadFromJsonAsync<ToDoDto>();
        updated.Should().NotBeNull();
        updated.Task.Should().Be(updateBody.Task);
    }

    [Fact]
    public async Task CompleteTodo_SetsCompletedDate()
    {
        await _client.PostAsJsonAsync("Profiles", new { });
        var createBody = new CreateTaskCommandBody("Complete test");
        var createResp = await _client.PostAsJsonAsync("ToDos", createBody);
        var created = await createResp.Content.ReadFromJsonAsync<ToDoDto>();

        var response = await _client.PutAsync($"ToDos/{created!.Id}/complete", null);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<ToDoDto>();
        dto.Should().NotBeNull();
        dto.CompletedDateTime.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task RemoveTodo_DeletesSuccessfully()
    {
        await _client.PostAsJsonAsync("Profiles", new { });
        var createBody = new CreateTaskCommandBody("Remove test");
        var createResp = await _client.PostAsJsonAsync("ToDos", createBody);
        var created = await createResp.Content.ReadFromJsonAsync<ToDoDto>();

        var deleteResp = await _client.DeleteAsync($"ToDos/{created!.Id}");
        deleteResp.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResp = await _client.GetAsync($"ToDos/{created.Id}");
        getResp.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}