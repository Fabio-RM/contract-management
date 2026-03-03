using Application.Clients.Commands;
using Application.Clients.Exceptions;
using Application.Common.Interfaces;
using Core.AggregateRoots;
using Core.ValueObjects;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests.Application.Clients.Commands;

public class DeactivateClientTests
{
    [Fact]
    public async Task Should_deactivate_client_if_it_exists()
    {
        var repository = new FakeClientsWriteRepository();
        
        var dateTimeMock = new Mock<IDateTimeProvider>();
        var fixedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var client = Client.Create(
            new ClientCnpj("12.123.456/0001-12"),
            new ClientName("John Doe")
            );
        
        dateTimeMock.Setup(d => d.UtcNow).Returns(fixedDate);

        await repository.AddAsync(client, CancellationToken.None);
        
        var handler = new DeactivateClient.Handler(repository, dateTimeMock.Object);
        var command = new DeactivateClient.Command(client.Id);
        
        await handler.Handle(command, CancellationToken.None);
        
        client.IsActive().Should().BeFalse();
        client.DeletedAt.Should().Be(fixedDate);
    }

    [Fact]
    public async Task Should_throw_exception_if_client_does_not_exist()
    {
        var repository = new FakeClientsWriteRepository();
        
        var dateTimeMock = new Mock<IDateTimeProvider>();
        var fixedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        dateTimeMock.Setup(d => d.UtcNow).Returns(fixedDate);
        
        var handler = new DeactivateClient.Handler(repository, dateTimeMock.Object);
        var command = new DeactivateClient.Command(Guid.NewGuid());
        
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ClientNotFoundException>();
    }
}