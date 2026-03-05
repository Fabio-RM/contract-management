using Application.Clients.Commands;
using Application.Clients.DTOs;
using Application.Clients.Exceptions;
using Application.Clients.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Tests.IntegrationTests.Clients;

[Collection("Database Collection")]
public class ClientRepositoryIntegrationTests : IClassFixture<DatabaseFixture>, IAsyncLifetime
{
    private readonly DatabaseFixture _fixture;
    private readonly ITestOutputHelper _output;

    public ClientRepositoryIntegrationTests(DatabaseFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture;
        _output = testOutputHelper;
    }

    public async Task InitializeAsync() => await _fixture.ResetDatabaseAsync();
    
    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Should_persist_client_when_command_is_executed()
    {
        using var scope = _fixture.ServiceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var command = new CreateClient.Command("12.456.789/0001-23", "John Doe");
        
        var result = await mediator.Send(command);
        
        var client = await db.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Cnpj.Value == "12456789000123");
        
        Assert.IsType<Guid>(result);
        
        client.Should().NotBeNull();
        client.Cnpj.Value.Should().Be("12456789000123");
        client.Name.Value.Should().Be("John Doe");
    }

    [Fact]
    public async Task Should_throw_exception_when_add_client_with_same_cnpj()
    {
        using var scope = _fixture.ServiceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var command = new CreateClient.Command("12.456.789/0001-23", "John Doe");
        
        await mediator.Send(command);
        
        Func<Task> act = async () => await mediator.Send(command);
    
        await act.Should().ThrowAsync<ClientAlreadyExistsException>();
    }

    [Fact]
    public async Task Should_retrieve_all_clients_when_query_is_executed()
    {
        using var scope = _fixture.ServiceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        await mediator.Send(new CreateClient.Command("12.456.789/0001-23", "John Doe"));
        await mediator.Send(new CreateClient.Command("12.456.789/0001-24", "Anna Doe"));
        
        var query = new GetAllClients.Query();
        var clients = await mediator.Send(query);
        
        clients.Should().NotBeNull();
        clients.TotalCount.Should().Be(2);
    }

    [Fact]
    public async Task Should_retrieve_all_clients_with_filters()
    {
        using var scope = _fixture.ServiceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        await mediator.Send(new CreateClient.Command("12.456.789/0001-23", "John Doe"));
        await mediator.Send(new CreateClient.Command("12.456.789/0001-24", "John Snow"));
        await mediator.Send(new CreateClient.Command("12.456.789/0001-25", "Anna Doe"));
        
        var query = new GetAllClients.Query(NameFilter: "John", OrderBy: "name", Descending: true);
        var clients = await mediator.Send(query);
        
        clients.Should().NotBeNull();
        clients.TotalCount.Should().Be(2);
        clients.Items[0].Name.Should().Be("John Snow");
        clients.Items[1].Name.Should().Be("John Doe");
    }
}