using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.Books.Queries.GetBooks;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Tests.Books.Helpers;

namespace XlibraryPro.Tests.Books.Queries;

public class GetBooksHandlerTests
{
    private readonly Mock<IBookRepository> _repo = new();
    private readonly GetBooksHandler      _sut;

    public GetBooksHandlerTests()
    {
        _sut = new GetBooksHandler(_repo.Object);
    }

    [Fact]
    public async Task Handle_ReturnsAllBooks()
    {
        // Arrange
        var books = BookFakes.MakeBooks(3).ToList();
        _repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(books);

        // Act
        var result = await _sut.Handle(new GetBooksQuery(), CancellationToken.None);

        // Assert
        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoBooksExist()
    {
        // Arrange
        _repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(Enumerable.Empty<XlibraryPro.Domain.Entities.Book>());

        // Act
        var result = await _sut.Handle(new GetBooksQuery(), CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_MapsTitleCorrectly()
    {
        // Arrange
        var book = BookFakes.MakeBook(title: "The Pragmatic Programmer");
        _repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(new[] { book });

        // Act
        var result = await _sut.Handle(new GetBooksQuery(), CancellationToken.None);

        // Assert
        result.First().Title.Should().Be("The Pragmatic Programmer");
    }

    [Fact]
    public async Task Handle_MapsIsbnCorrectly()
    {
        // Arrange
        var book = BookFakes.MakeBook();
        _repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(new[] { book });

        // Act
        var result = await _sut.Handle(new GetBooksQuery(), CancellationToken.None);

        // Assert
        result.First().Isbn.Should().Be("9780132350884");
    }
}