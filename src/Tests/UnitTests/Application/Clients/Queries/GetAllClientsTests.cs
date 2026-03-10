using Application.Clients.DTOs;
using Application.Clients.Interfaces.Repositories;
using Application.Clients.Queries;
using Application.Common.Pagination;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests.Application.Clients.Queries;

public class GetAllClientsTests
{
    private ClientDto[] _clientsDtoToAdd =
    {
        new ClientDto(
            Id: Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Cnpj: "11.111.111/0001-11",
            Name: "John Doe",
            Status: "Active"), 
        new ClientDto(
            Id: Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Cnpj: "22.222.222/0001-22",
            Name: "Anna Doe",
            Status: "Active")
    };

    [Fact]
    public async Task Should_return_all_clients_list()
    {
        var repository = new FakeClientsQueryRepository(_clientsDtoToAdd);
        
        var expectedResults = new PagedResults<ClientDto>(
            items: new List<ClientDto>{_clientsDtoToAdd[0], _clientsDtoToAdd[1]},
            totalCount: 2,
            currentPage: 1,
            pageSize: 10
        );
        
        var handler = new GetAllClients.Handler(repository);
        var query = new GetAllClients.Query();
        
        var result = await handler.Handle(query, CancellationToken.None);
        var clients = result.Value;
        
        result.IsSuccess.Should().BeTrue();
        clients.Should().BeEquivalentTo(expectedResults);
    }

    [Fact]
    public async Task Should_return_all_clients_with_filters()
    {
        var repository = new FakeClientsQueryRepository(_clientsDtoToAdd);
        
        var expectedResults = new PagedResults<ClientDto>(
            items: new List<ClientDto>{_clientsDtoToAdd[0]},
            totalCount: 1,
            currentPage: 1,
            pageSize: 10
        );
        
        var handler = new GetAllClients.Handler(repository);
        var query = new GetAllClients.Query
        {
            CnpjFilter = "11.111.111/0001-11",
        };
        
        var result = await handler.Handle(query, CancellationToken.None);
        var clients = result.Value;
        
        result.IsSuccess.Should().BeTrue();
        clients.Should().BeEquivalentTo(expectedResults);
    }
}