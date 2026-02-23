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
        ClientCnpj cnpj = new ClientCnpj("12.456.789/0001-12");
        ClientName clientName = new ClientName("John Doe");
        
        Client c = Client.Create(cnpj, clientName);
        
        c.Deactivate();
        
        c.IsActive().Should().BeFalse();
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
        ClientCnpj cnpj = new ClientCnpj("12.456.789/0001-12");
        ClientName clientName = new ClientName("John Doe");
        
        Client c = Client.Create(cnpj, clientName);
        c.Deactivate();
        
        Action act = () => c.Deactivate();
        
        act.Should().Throw<ClientInactiveException>();
    }
}