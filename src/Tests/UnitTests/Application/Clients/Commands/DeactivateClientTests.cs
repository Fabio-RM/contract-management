using Application.Clients.Commands;
using Application.Common.Interfaces;
using Core.AggregateRoots;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests.Application.Clients.Commands;

public class DeactivateClientTests
{
    [Fact]
    public async Task Should_deactivate_client_if_it_exists()
    {
        var repository = new FakeClientsWriteRepository();
        
        var resultClient = Client.Create("12.123.456/0001-12", "John Doe");
        var client = resultClient.Value;
        
        await repository.AddAsync(client, CancellationToken.None);
        
        var handler = new DeactivateClient.Handler(repository);
        var command = new DeactivateClient.Command(client.Id);
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        client.IsActive().Should().BeFalse();
        client.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task Should_fail_if_client_does_not_exist()
    {
        var repository = new FakeClientsWriteRepository();
        
        var dateTimeMock = new Mock<IDateTimeProvider>();
        var fixedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        dateTimeMock.Setup(d => d.UtcNow).Returns(fixedDate);
        
        var handler = new DeactivateClient.Handler(repository);
        var command = new DeactivateClient.Command(Guid.NewGuid());
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}