using Application.Clients.DTOs;
using Application.Clients.Interfaces.Repositories;
using Application.Clients.Queries.GetAllClients;
using Application.Common.Pagination;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests.Application.Clients.Queries;

public class GetAllClientsTests
{
    [Fact]
    public async Task Should_return_all_clients_list()
    {
        var repositoryMock = new Mock<IClientQueryRepository>();

        var pagedResults = new PagedResults<ClientDto>(
            items: new List<ClientDto>{
                new ClientDto(
                    Id: new Guid(),
                    Cnpj: "12.456.789/0001-12",
                    Name: "John Doe",
                    Status: "Active")
                },
            totalCount: 1,
            currentPage: 1,
            pageSize: 10
        );
        
        repositoryMock
            .Setup(repo => repo.GetAllClientsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagedResults);
        
        var handler = new GetAllClientsQueryHandler(repositoryMock.Object);
        var query = new GetAllClientsQuery();
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        result.Should().BeEquivalentTo(pagedResults);
        
        repositoryMock.Verify(repo => repo.GetAllClientsAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}