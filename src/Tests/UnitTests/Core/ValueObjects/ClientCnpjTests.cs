using Core.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Tests.UnitTests.Core.ValueObjects;

public class ClientCnpjTests
{
    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("12.345.678/0001-AA")]
    [InlineData("123456780001AA")]
    [InlineData("12.345.678/0001-1")]
    [InlineData("1234567800011")]
    [InlineData("12.345.678/0001-123")]
    [InlineData("123456780001123")]
    public void Should_throw_exception_when_cnpj_is_invalid(string invalidCnpj)
    {
        Action act = () => new ClientCnpj(invalidCnpj);

        act.Should().Throw<Exception>();
    }
    
    [Theory]
    [InlineData("12.456.789/0001-12")]
    [InlineData("12456789000112")]
    public void Should_create_client_cnpj_when_valid(string validCnpj)
    {
        var cnpj = new ClientCnpj(validCnpj);

        cnpj.Cnpj.Should().Be("12456789000112");
    }
    
    [Fact]
    public void Two_equal_cnpjs_should_be_equal()
    {
        var cnpj1 = new ClientCnpj("12456789000112");
        var cnpj2 = new ClientCnpj("12.456.789/0001-12");

        cnpj1.Equals(cnpj2).Should().BeTrue();
    }
} 