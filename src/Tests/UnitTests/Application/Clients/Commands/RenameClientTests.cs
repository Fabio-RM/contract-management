using Application.Clients.Commands;
using Application.Clients.Exceptions;
using Core.AggregateRoots;
using Core.ValueObjects;
using FluentAssertions;
using MediatR;

namespace Tests.UnitTests.Application.Clients.Commands;

public class RenameClientTests
{
    [Fact]
    public async Task Should_rename_client_and_save_changes()
    {
        var repository = new FakeClientsWriteRepository();
        
        Client client = Client.Create(
            new ClientCnpj("12.123.456/0001-12"),
            new ClientName("John Doe")
            );
        
        await repository.AddAsync(client, CancellationToken.None);
        
        var handler = new RenameClient.Handler(repository);
        
        var command = new RenameClient.Command(
            ClientId: client.Id, 
            NewName: "New Name");
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Should().Be(Unit.Value);
        client.Name.Value.Should().Be("New Name");
    }

    [Fact]
    public async Task Should_throw_exception_if_client_not_found()
    {
        var repository = new FakeClientsWriteRepository();
        
        var handler = new RenameClient.Handler(repository);
        
        var command = new RenameClient.Command(
            ClientId: Guid.NewGuid(),
            NewName: "New Name");
        
        Func<Task> act = async () =>  await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ClientNotFoundException>();
    }
}