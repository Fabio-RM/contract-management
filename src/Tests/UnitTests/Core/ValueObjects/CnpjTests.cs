using Core.DomainErrors;
using Core.ValueObjects;
using FluentAssertions;

namespace Tests.UnitTests.Core.ValueObjects;

public class CnpjTests
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
    public void Should_failure_when_cnpj_is_invalid(string invalidCnpj)
    {
        var result = Cnpj.Create(invalidCnpj);
        
        result.IsFailure.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("12.456.789/0001-12")]
    [InlineData("12456789000112")]
    public void Should_create_client_cnpj_when_valid(string validCnpj)
    {
        var result = Cnpj.Create(validCnpj);
        var cnpj = result.Value;
        
        result.IsSuccess.Should().BeTrue();
        cnpj.Value.Should().Be("12456789000112");
    }
    
    [Fact]
    public void Two_equal_cnpjs_should_be_equal()
    {
        var resultCnpj1 = Cnpj.Create("12456789000112");
        var resultCnpj2 = Cnpj.Create("12.456.789/0001-12");

        var cnpj1 = resultCnpj1.Value;
        var cnpj2 = resultCnpj2.Value;
        
        cnpj1.Equals(cnpj2).Should().BeTrue();
    }
}