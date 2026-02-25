using Application.Clients.Commands;
using Application.Clients.Exceptions;
using Core.AggregateRoots;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;

namespace Tests.UnitTests.Application.Clients.Commands;

public class RenameClientTests
{
    [Fact]
    public async Task Should_rename_client_and_save_changes()
    {
        var repositoryMock = new Mock<IClientRepository>();
        
        Client client = Client.Create(
            new ClientCnpj("12.123.456/0001-12"),
            new ClientName("John Doe")
            );
        
        repositoryMock.Setup(repo => repo.GetByIdAsync(
            client.Id, 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);
        
        var handler = new RenameClient.Handler(repositoryMock.Object);
        
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
        var repositoryMock = new Mock<IClientRepository>();
        
        repositoryMock
            .Setup(repo => repo.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(Client));
        
        var handler = new RenameClient.Handler(repositoryMock.Object);
        
        var command = new RenameClient.Command(
            ClientId: Guid.NewGuid(),
            NewName: "New Name");
        
        Func<Task> act = async () =>  await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ClientNotFoundException>();
    }
}