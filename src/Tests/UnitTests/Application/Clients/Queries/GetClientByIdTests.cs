using Application.Clients.Exceptions;
using Application.Clients.Interfaces.Repositories;
using Application.Clients.Queries.GetClientById;
using Application.Clients.Queries.Models;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests.Application.Clients.Queries;

public class GetClientByIdTests
{
    [Fact]
    public async Task Should_return_client_dto_when_client_exists()
    {
        var repositoryMock = new Mock<IClientQueryRepository>();

        var readModel = new ClientReadModel
        {
            Id = Guid.NewGuid(),
            Cnpj = "12.123.456-12",
            Name = "John Doe",
            Status = "Active"
        };

        repositoryMock
            .Setup(repo => repo.GetByIdAsync(readModel.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readModel);
        
        var handler = new GetClientByIdQueryHandler(repositoryMock.Object);
        var query = new GetClientByIdQuery(readModel.Id);
        
        var client = await handler.Handle(query, CancellationToken.None);
        
        client.Should().NotBeNull();
        client.Id.Should().Be(readModel.Id);
        client.Cnpj.Should().Be(readModel.Cnpj);
        client.Name.Should().Be(readModel.Name);
        client.Status.Should().Be(readModel.Status);
        
        repositoryMock.Verify(repo => repo.GetByIdAsync(readModel.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_throw_exception_when_client_not_found()
    {
        var repositoryMock = new Mock<IClientQueryRepository>();
        
        repositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ClientReadModel?)null);
        
        var handler = new GetClientByIdQueryHandler(repositoryMock.Object);
        var query = new GetClientByIdQuery(Guid.NewGuid());
        
        Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<ClientNotFoundException>();
    }
}