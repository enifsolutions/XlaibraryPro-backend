using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.Books.Queries.GetBookById;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Tests.Books.Helpers;

namespace XlibraryPro.Tests.Books.Queries;

public class GetBookByIdHandlerTests
{
    private readonly Mock<IBookRepository> _repo = new();
    private readonly GetBookByIdHandler    _sut;

    public GetBookByIdHandlerTests()
    {
        _sut = new GetBookByIdHandler(_repo.Object);
    }

    [Fact]
    public async Task Handle_ReturnsBook_WhenFound()
    {
        // Arrange
        var book = BookFakes.MakeBook(id: 1, title: "Clean Code");
        _repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
             .ReturnsAsync(book);

        // Act
        var result = await _sut.Handle(new GetBookByIdQuery(1), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("Clean Code");
        result.BookId.Should().Be(1);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenNotFound()
    {
        // Arrange
        _repo.Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
             .ReturnsAsync((XlibraryPro.Domain.Entities.Book?)null);

        // Act
        var result = await _sut.Handle(new GetBookByIdQuery(99), CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_MapsAllFieldsCorrectly()
    {
        // Arrange
        var book = BookFakes.MakeBook(id: 1);
        _repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
             .ReturnsAsync(book);

        // Act
        var result = await _sut.Handle(new GetBookByIdQuery(1), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Isbn.Should().Be("9780132350884");
        result.Pages.Should().Be(431);
        result.PublicationYear.Should().Be("2008");
        result.CopyrightYear.Should().Be(2008);
        result.PlaceOfPublication.Should().Be("Upper Saddle River");
        result.EditionStatement.Should().Be("1st ed.");
    }
}