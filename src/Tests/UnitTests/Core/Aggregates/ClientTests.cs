using Core.AggregateRoots;
using Core.DomainErrors;
using Core.Exceptions;
using Core.ValueObjects;
using FluentAssertions;

namespace Tests.UnitTests.Core.Aggregates;

public class ClientTests
{
    [Fact]
    public void Should_create_client_as_active()
    {
        var cnpjResult = Cnpj.Create("12.456.789/0001-12");
        var nameResult = Name.Create("John Doe");
        
        var cnpj = cnpjResult.Value;
        var name = nameResult.Value;
        
        var result = Client.Create(cnpj.Value, name.Value);
        
        result.Value.IsActive().Should().BeTrue();
    }

    [Fact]
    public void Should_active_client_be_deactivated()
    {
        var fixedDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var cnpjResult = Cnpj.Create("12.456.789/0001-12");
        var nameResult = Name.Create("John Doe");
        
        var cnpj = cnpjResult.Value;
        var name = nameResult.Value;
        
        var result = Client.Create(cnpj.Value, name.Value);
        var client = result.Value;
        
        client.Deactivate(fixedDate);
        
        result.IsSuccess.Should().BeTrue();
        client.IsActive().Should().BeFalse();
        client.DeletedAt.Should().Be(fixedDate);
    }

    [Fact]
    public void Should_not_activate_client_already_active()
    {
        var cnpjResult = Cnpj.Create("12.456.789/0001-12");
        var nameResult = Name.Create("John Doe");
        
        var cnpj = cnpjResult.Value;
        var name = nameResult.Value;
        
        var clientResult = Client.Create(cnpj.Value, name.Value);
        var client = clientResult.Value;

        var result = client.Activate();
        
        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public void Should_not_deactivate_client_already_inactive()
    {
        var fixedDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var cnpjResult = Cnpj.Create("12.456.789/0001-12");
        var nameResult = Name.Create("John Doe");
        
        var cnpj = cnpjResult.Value;
        var name = nameResult.Value;
        
        var clientResult = Client.Create(cnpj.Value, name.Value);
        var client = clientResult.Value;
        
        client.Deactivate(fixedDate);
        
        var result = client.Deactivate(fixedDate);
        
        result.IsFailure.Should().BeTrue();
    }
}