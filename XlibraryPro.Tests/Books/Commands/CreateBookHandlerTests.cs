using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.Books.Commands.CreateBook;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Tests.Books.Commands;

public class CreateBookHandlerTests
{
    private readonly Mock<IBookRepository> _repo = new();
    private readonly CreateBookHandler     _sut;

    public CreateBookHandlerTests()
    {
        _sut = new CreateBookHandler(_repo.Object);
    }

    private static CreateBookCommand ValidCommand() => new(
        Title:             "Clean Architecture",
        PrimaryLanguageId: 1,
        BookTypeId:        1,
        DeweyId:           1,
        PublisherId:       1,
        Isbn:              "9780134494166",
        Description:       "A craftsman's guide.",
        Pages:             432,
        PlaceOfPublication:"Upper Saddle River",
        PublicationYear:   "2017",
        CopyrightYear:     2017,
        EditionStatement:  "1st ed.",
        Notes:             null
    );

    [Fact]
    public async Task Handle_CallsAddAsync_Once()
    {
        // Arrange
        _repo.Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
             .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(ValidCommand(), CancellationToken.None);

        // Assert
        _repo.Verify(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CreatesBookWithCorrectTitle()
    {
        // Arrange
        Book? captured = null;
        _repo.Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
             .Callback<Book, CancellationToken>((b, _) => captured = b)
             .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(ValidCommand(), CancellationToken.None);

        // Assert
        captured.Should().NotBeNull();
        captured!.Title.Should().Be("Clean Architecture");
    }

    [Fact]
    public async Task Handle_CreatesBookWithValidIsbn()
    {
        // Arrange
        Book? captured = null;
        _repo.Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
             .Callback<Book, CancellationToken>((b, _) => captured = b)
             .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(ValidCommand(), CancellationToken.None);

        // Assert
        captured!.Isbn.Should().NotBeNull();
        captured.Isbn!.Value.Should().Be("9780134494166");
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRepositoryFails()
    {
        // Arrange
        _repo.Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
             .ThrowsAsync(new Exception("sp_action_book returned error on ADD."));

        // Act
        var act = () => _sut.Handle(ValidCommand(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("*sp_action_book*");
    }
}