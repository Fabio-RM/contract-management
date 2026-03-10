using Application.Clients.Commands;
using Core.AggregateRoots;
using Core.ValueObjects;
using FluentAssertions;

namespace Tests.UnitTests.Application.Clients.Commands;

public class RenameClientTests
{
    [Fact]
    public async Task Should_rename_client_and_save_changes()
    {
        var repository = new FakeClientsWriteRepository();
        
        var client = Client.Create("12.123.456/0001-12","John Doe").Value;
        
        await repository.AddAsync(client, CancellationToken.None);
        
        var handler = new RenameClient.Handler(repository);
        
        var command = new RenameClient.Command(
            ClientId: client.Id, 
            NewName: "New ClientName");
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        client.ClientName.Value.Should().Be("New ClientName");
    }

    [Fact]
    public async Task Should_fail_if_client_not_found()
    {
        var repository = new FakeClientsWriteRepository();
        
        var handler = new RenameClient.Handler(repository);
        
        var command = new RenameClient.Command(
            ClientId: Guid.NewGuid(),
            NewName: "John Doe");
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Errors.Code.Should().Be("Client.NotFound");
    }
}