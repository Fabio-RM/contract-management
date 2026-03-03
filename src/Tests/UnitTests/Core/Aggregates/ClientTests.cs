using Core.AggregateRoots;
using Core.Exceptions;
using Core.ValueObjects;
using FluentAssertions;

namespace Tests.UnitTests.Core.Aggregates;

public class ClientTests
{
    [Fact]
    public void Should_create_client_as_active()
    {
        ClientCnpj cnpj = new ClientCnpj("12.456.789/0001-12");
        ClientName clientName = new ClientName("John Doe");
        
        Client c = Client.Create(cnpj, clientName);
        
        c.IsActive().Should().BeTrue();
    }

    [Fact]
    public void Should_active_client_be_deactivated()
    {
        var fixedDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        ClientCnpj cnpj = new ClientCnpj("12.456.789/0001-12");
        ClientName clientName = new ClientName("John Doe");
        
        Client c = Client.Create(cnpj, clientName);
        
        c.Deactivate(fixedDate);
        
        c.IsActive().Should().BeFalse();
        c.DeletedAt.Should().Be(fixedDate);
    }

    [Fact]
    public void Should_not_activate_client_already_active()
    {
        ClientCnpj cnpj = new ClientCnpj("12.456.789/0001-12");
        ClientName clientName = new ClientName("John Doe");
        
        Client c = Client.Create(cnpj, clientName);
        
        Action act = () => c.Activate();
        
        act.Should().Throw<ClientActiveException>();
    }
    
    [Fact]
    public void Should_not_deactivate_client_already_inactive()
    {
        var fixedDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        ClientCnpj cnpj = new ClientCnpj("12.456.789/0001-12");
        ClientName clientName = new ClientName("John Doe");
        
        Client c = Client.Create(cnpj, clientName);
        c.Deactivate(fixedDate);
        
        Action act = () => c.Deactivate(fixedDate);
        
        act.Should().Throw<ClientInactiveException>();
    }
}