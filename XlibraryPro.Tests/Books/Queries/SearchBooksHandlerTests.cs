using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.Books.Queries.SearchBooks;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Tests.Books.Helpers;

namespace XlibraryPro.Tests.Books.Queries;

public class SearchBooksHandlerTests
{
    private readonly Mock<IBookRepository> _repo = new();
    private readonly SearchBooksHandler    _sut;

    public SearchBooksHandlerTests()
    {
        _sut = new SearchBooksHandler(_repo.Object);
    }

    [Fact]
    public async Task Handle_ReturnsMatchingBooks()
    {
        // Arrange
        var books = BookFakes.MakeBooks(2).ToList();
        _repo.Setup(r => r.SearchAsync("clean", It.IsAny<CancellationToken>()))
             .ReturnsAsync(books);

        // Act
        var result = await _sut.Handle(new SearchBooksQuery("clean"), CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_ReturnsEmpty_WhenNoMatch()
    {
        // Arrange
        _repo.Setup(r => r.SearchAsync("zzznomatch", It.IsAny<CancellationToken>()))
             .ReturnsAsync(Enumerable.Empty<XlibraryPro.Domain.Entities.Book>());

        // Act
        var result = await _sut.Handle(new SearchBooksQuery("zzznomatch"), CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_PassesSearchTermToRepository()
    {
        // Arrange
        _repo.Setup(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(Enumerable.Empty<XlibraryPro.Domain.Entities.Book>());

        // Act
        await _sut.Handle(new SearchBooksQuery("dapper"), CancellationToken.None);

        // Assert
        _repo.Verify(r => r.SearchAsync("dapper", It.IsAny<CancellationToken>()), Times.Once);
    }
}