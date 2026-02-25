using Application.Clients.Commands.DeactivateClient;
using Application.Clients.Exceptions;
using Application.Common;
using Application.Common.Interfaces;
using Core.AggregateRoots;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit.Abstractions;

namespace Tests.UnitTests.Application.Clients.Commands;

public class DeactivateClientHandlerTests
{
    [Fact]
    public async Task Should_deactivate_client_if_it_exists()
    {
        var repositoryMock = new Mock<IClientRepository>();
        
        var dateTimeMock = new Mock<IDateTimeProvider>();
        var fixedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var client = Client.Create(
            new ClientCnpj("12.123.456/0001-12"),
            new ClientName("John Doe")
            );
        
        dateTimeMock.Setup(d => d.UtcNow).Returns(fixedDate);
        
        repositoryMock
            .Setup(repo => repo.GetByIdAsync(
                It.Is<Guid>(id => id == client.Id), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);
        
        var handler = new DeactivateClientCommandHandler(repositoryMock.Object, dateTimeMock.Object);
        var command = new DeactivateClientCommand(client.Id);
        
        await handler.Handle(command, CancellationToken.None);
        
        client.IsActive().Should().BeFalse();
        client.DeletedAt.Should().Be(fixedDate);
        
        repositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_throw_exception_if_client_does_not_exist()
    {
        var repositoryMock = new Mock<IClientRepository>();
        
        var dateTimeMock = new Mock<IDateTimeProvider>();
        var fixedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        repositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Client?)null);
        
        dateTimeMock.Setup(d => d.UtcNow).Returns(fixedDate);
        
        var handler = new DeactivateClientCommandHandler(repositoryMock.Object, dateTimeMock.Object);
        var command = new DeactivateClientCommand(Guid.NewGuid());
        
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ClientNotFoundException>();
        
        repositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}