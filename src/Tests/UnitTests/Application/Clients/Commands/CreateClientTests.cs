using Application.Clients.Commands;
using Application.Clients.Exceptions;
using FluentAssertions;

namespace Tests.UnitTests.Application.Clients.Commands;

public class CreateClientTests
{
    [Fact]
    public async Task Should_create_client_and_add_to_repository()
    {
        var repository = new FakeClientsWriteRepository();
        
        var handler = new CreateClient.Handler(repository);

        var command = new CreateClient.Command(
            Cnpj: "12.123.123/0001-12",
            Name: "John Doe"
        );
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Should().NotBeEmpty();
        result.GetType().Should().Be(typeof(Guid));
    }

    [Fact]
    public async Task Should_throw_exception_when_cnpj_is_invalid()
    {
        var repository = new FakeClientsWriteRepository();
        
        var handler = new CreateClient.Handler(repository);

        var command = new CreateClient.Command(
            Cnpj: "INVALID",
            Name: "John Doe");
        
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Should_not_add_client_with_same_cnpj_to_repository()
    {
        var repository = new FakeClientsWriteRepository();
        
        var handler = new CreateClient.Handler(repository);

        var command = new CreateClient.Command(
            Cnpj: "12.123.123/0001-12",
            Name: "John Doe"
        );
        
        await handler.Handle(command, CancellationToken.None);
        
        // Try to add the same client again
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<ClientAlreadyExistsException>();
    }
}