using System;
using Core.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Tests.UnitTests.Core.ValueObjects;

public class NameTests
{
    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void Should_result_failure_when_name_is_invalid(string invalidName)
    {
        var result = Name.Create(invalidName);
        
        result.IsFailure.Should().BeTrue();
    }

    [Theory]
    [InlineData("John Doe")]
    [InlineData("Anna")]
    public void Should_create_client_name_when_valid(string validName)
    {
        var result = Name.Create(validName);
        var name =  result.Value;
        
        result.IsSuccess.Should().BeTrue();
        name.Value.Should().Be(validName);
    }

    [Fact]
    public void Two_equal_names_should_be_equal()
    {
        var resultName1 = Name.Create("John Doe");
        var resultName2 = Name.Create("   John Doe    ");
        
        var name1 = resultName1.Value;
        var name2 = resultName2.Value;
        
        name1.Equals(name2).Should().BeTrue();
    }
}