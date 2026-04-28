using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.Books.Commands.DeleteBook;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Tests.Books.Commands;

public class DeleteBookHandlerTests
{
    private readonly Mock<IBookRepository> _repo = new();
    private readonly DeleteBookHandler     _sut;

    public DeleteBookHandlerTests()
    {
        _sut = new DeleteBookHandler(_repo.Object);
    }

    [Fact]
    public async Task Handle_CallsDeleteAsync_WithCorrectId()
    {
        // Arrange
        _repo.Setup(r => r.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
             .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(new DeleteBookCommand(99), CancellationToken.None);

        // Assert
        _repo.Verify(r => r.DeleteAsync(99, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRepositoryFails()
    {
        // Arrange
        _repo.Setup(r => r.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
             .ThrowsAsync(new Exception("sp_action_book returned error on DELETE."));

        // Act
        var act = () => _sut.Handle(new DeleteBookCommand(1), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("*DELETE*");
    }
}