using Application.Clients.Commands;
using FluentAssertions;
using Xunit.Abstractions;

namespace Tests.UnitTests.Application.Clients.Commands;

public class CreateClientTests
{
    [Fact]
    public async Task Should_create_client_and_add_to_repository()
    {
        var repository = new FakeClientsWriteRepository();
        
        var handler = new CreateClient.Handler(repository);
        var command = new CreateClient.Command("12.123.123/0001-12", "John Doe");
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        
        var clientId = result.Value;
        
        result.IsSuccess.Should().BeTrue();
        clientId.GetType().Should().Be(typeof(Guid));
    }

    [Fact]
    public async Task Should_fail_when_cnpj_is_invalid()
    {
        var repository = new FakeClientsWriteRepository();
        
        var handler = new CreateClient.Handler(repository);

        var command = new CreateClient.Command(
            Cnpj: "INVALID",
            Name: "John Doe");
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Should_fail_when_add_client_with_same_cnpj()
    {
        var repository = new FakeClientsWriteRepository();
        
        var handler = new CreateClient.Handler(repository);

        var command = new CreateClient.Command(
            Cnpj: "12.123.123/0001-12",
            Name: "John Doe"
        );
        
        await handler.Handle(command, CancellationToken.None);
        
        // Try to add the same client again
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.IsFailure.Should().BeTrue();
    }
}