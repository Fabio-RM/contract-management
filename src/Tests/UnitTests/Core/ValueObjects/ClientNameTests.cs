using Core.ValueObjects;
using FluentAssertions;

namespace Tests.UnitTests.Core.ValueObjects;

public class ClientNameTests
{
    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void Should_throw_exception_when_name_is_invalid(string invalidName)
    {
        Action act = () => new ClientName(invalidName);
        
        act.Should().Throw<Exception>();
    }

    [Theory]
    [InlineData("John Doe")]
    [InlineData("Anna")]
    public void Should_create_client_name_when_valid(string validName)
    {
        var name = new ClientName(validName);
        
        name.Name.Should().Be(validName);
    }

    [Fact]
    public void Two_equal_names_should_be_equal()
    {
        var name1 = new ClientName("John Doe");
        var name2 = new ClientName("John Doe");
        
        name1.Equals(name2).Should().BeTrue();
    }
}