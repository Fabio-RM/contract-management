using Application.Clients.DTOs;
using Application.Clients.Exceptions;
using Application.Clients.Interfaces.Repositories;
using Application.Clients.Queries;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests.Application.Clients.Queries;

public class GetClientByIdTests
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
    public async Task Should_return_client_dto_when_client_exists()
    {
        var repository = new FakeClientsQueryRepository(_clientsDtoToAdd);

        var clientDto = _clientsDtoToAdd[0];
        
        var handler = new GetClientById.Handler(repository);
        var query = new GetClientById.Query(clientDto.Id);
        
        var result = await handler.Handle(query, CancellationToken.None);
        var client = result.Value;
        
        result.IsSuccess.Should().BeTrue();
        client.Should().NotBeNull();
        client.Id.Should().Be(clientDto.Id);
        client.Cnpj.Should().Be(clientDto.Cnpj);
        client.Name.Should().Be(clientDto.Name);
        client.Status.Should().Be(clientDto.Status);
    }

    [Fact]
    public async Task Should_fail_when_client_not_found()
    {
        var repository = new FakeClientsQueryRepository(_clientsDtoToAdd);
        
        var handler = new GetClientById.Handler(repository);
        var query = new GetClientById.Query(Guid.NewGuid());
        
        var result = await handler.Handle(query, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}