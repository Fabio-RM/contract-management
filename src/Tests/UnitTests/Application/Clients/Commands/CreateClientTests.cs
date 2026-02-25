using Application.Clients.Commands;
using Application.Clients.Exceptions;
using Core.AggregateRoots;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests.Application.Clients.Commands;

public class CreateClientTests
{
    [Fact]
    public async Task Should_create_client_and_add_to_repository()
    {
        var repositoryMock = new Mock<IClientRepository>();
        
        repositoryMock
            .Setup(repo => repo.AddAsync(
                It.IsAny<Client>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        var handler = new CreateClient.Handler(repositoryMock.Object);

        var command = new CreateClient.Command(
            Cnpj: "12.123.123/0001-12",
            Name: "John Doe"
        );
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        
        repositoryMock.Verify(repo => repo.AddAsync(
            It.IsAny<Client>(), 
            It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task Should_throw_exception_when_cnpj_is_invalid()
    {
        var repositoryMock = new Mock<IClientRepository>();
        
        repositoryMock
            .Setup(repo => repo.AddAsync(
                It.IsAny<Client>(), 
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        var handler = new CreateClient.Handler(repositoryMock.Object);

        var command = new CreateClient.Command(
            Cnpj: "INVALID",
            Name: "John Doe");
        
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<Exception>();
        
        repositoryMock.Verify(
            repo => repo.AddAsync(It.IsAny<Client>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
    
    [Fact]
    public async Task Should_not_add_client_with_same_cnpj_to_repository()
    {
        var repositoryMock = new Mock<IClientRepository>();
        
        repositoryMock
            .Setup(repo => repo.ExistsByCnpjAsync(
                It.IsAny<ClientCnpj>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        var handler = new CreateClient.Handler(repositoryMock.Object);

        var command = new CreateClient.Command(
            Cnpj: "12.123.123/0001-12",
            Name: "John Doe"
        );
        
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ClientAlreadyExistsException>();
        
        repositoryMock.Verify(
            repo => repo.AddAsync(It.IsAny<Client>(), 
                It.IsAny<CancellationToken>()), 
            Times.Never);
    }
}