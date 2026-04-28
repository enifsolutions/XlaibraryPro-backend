using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.Books.Commands.UpdateBook;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Tests.Books.Commands;

public class UpdateBookHandlerTests
{
    private readonly Mock<IBookRepository> _repo = new();
    private readonly UpdateBookHandler     _sut;

    public UpdateBookHandlerTests()
    {
        _sut = new UpdateBookHandler(_repo.Object);
    }

    private static UpdateBookCommand ValidCommand(long id = 1) => new(
        Id:                id,
        Title:             "Updated Title",
        PrimaryLanguageId: 1,
        BookTypeId:        1,
        DeweyId:           1,
        PublisherId:       1
    );

    [Fact]
    public async Task Handle_CallsUpdateAsync_Once()
    {
        // Arrange
        _repo.Setup(r => r.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
             .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(ValidCommand(), CancellationToken.None);

        // Assert
        _repo.Verify(r => r.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdatesBookWithCorrectId()
    {
        // Arrange
        Book? captured = null;
        _repo.Setup(r => r.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
             .Callback<Book, CancellationToken>((b, _) => captured = b)
             .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(ValidCommand(id: 42), CancellationToken.None);

        // Assert
        captured!.Id.Should().Be(42);
        captured.Title.Should().Be("Updated Title");
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRepositoryFails()
    {
        // Arrange
        _repo.Setup(r => r.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
             .ThrowsAsync(new Exception("sp_action_book returned error on UPDATE."));

        // Act
        var act = () => _sut.Handle(ValidCommand(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("*UPDATE*");
    }
}