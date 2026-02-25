using Application.Clients.Commands.RemoveClient;
using Application.Clients.Exceptions;
using Core.AggregateRoots;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit.Abstractions;

namespace Tests.UnitTests.Application.Clients.Commands;

public class DeleteClientHandlerTests
{
    [Fact]
    public async Task Should_delete_client_if_it_exists()
    {
        var repositoryMock = new Mock<IClientRepository>();
        
        var client = Client.Create(
            new ClientCnpj("12.123.456/0001-12"),
            new ClientName("John Doe")
            );
        
        repositoryMock
            .Setup(repo => repo.GetByIdAsync(
                It.Is<Guid>(id => id == client.Id), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);
        
        var handler = new RemoveClientCommandHandler(repositoryMock.Object);
        var command = new RemoveClientCommand(client.Id);
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Should().Be(Unit.Value);
        
        repositoryMock.Verify(repo => repo.GetByIdAsync(
            It.Is<Guid>(id => id == client.Id),
            It.IsAny<CancellationToken>()), Times.Once);
        
        repositoryMock.Verify(repo => repo.RemoveAsync(client, CancellationToken.None), Times.Once);
        
        repositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_throw_exception_if_client_does_not_exist()
    {
        var repositoryMock = new Mock<IClientRepository>();
        
        repositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Client?)null);
        
        var handler = new RemoveClientCommandHandler(repositoryMock.Object);
        var command = new RemoveClientCommand(Guid.NewGuid());
        
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ClientNotFoundException>();
        
        repositoryMock.Verify(repo => repo.RemoveAsync(It.IsAny<Client>(), CancellationToken.None), Times.Never);
        repositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}