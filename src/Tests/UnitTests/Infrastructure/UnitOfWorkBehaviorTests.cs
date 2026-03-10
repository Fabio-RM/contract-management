using Application.Common.Behaviors;
using Application.Common.Interfaces;
using FluentAssertions;
using MediatR;
using Moq;

namespace Tests.UnitTests.Infrastructure;

public class UnitOfWorkBehaviorTests
{
    private class FakeCommand : ICommand<string>;

    [Fact]
    public async Task Should_call_commit_after_handler()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var behavior = new UnitOfWorkBehavior<FakeCommand, string>(unitOfWorkMock.Object);

        var command = new FakeCommand();
        
        var nextMock = new Mock<RequestHandlerDelegate<string>>();

        nextMock.Setup(n => n())
            .ReturnsAsync("ok");
        
        var response = await behavior.Handle(command, nextMock.Object, CancellationToken.None);
        
        response.Should().Be("ok");
        
        nextMock.Verify(n => n(), Times.Once);
        
        unitOfWorkMock.Verify(
            u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}