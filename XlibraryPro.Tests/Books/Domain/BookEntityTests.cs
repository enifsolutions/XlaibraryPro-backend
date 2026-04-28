using FluentAssertions;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Enums;
using XlibraryPro.Tests.Books.Helpers;

namespace XlibraryPro.Tests.Books.Domain;

public class BookEntityTests
{
    [Fact]
    public void Constructor_CreatesBook_WithCorrectTitle()
    {
        var book = BookFakes.MakeBook(title: "DDIA");
        book.Title.Should().Be("DDIA");
    }

    [Fact]
    public void SetTitle_Throws_WhenTitleIsEmpty()
    {
        var book = BookFakes.MakeBook();
        var act  = () => book.SetTitle("");
        act.Should().Throw<ArgumentException>().WithMessage("*title*");
    }

    [Fact]
    public void SetTitle_TrimsWhitespace()
    {
        var book = BookFakes.MakeBook();
        book.SetTitle("  Clean Code  ");
        book.Title.Should().Be("Clean Code");
    }

    [Fact]
    public void AddAuthor_AddsAuthorToCollection()
    {
        var book = BookFakes.MakeBook();
        book.AddAuthor(1, "Author");
        book.Authors.Should().HaveCount(1);
    }

    [Fact]
    public void AddAuthor_IsIdempotent_ForSameAuthorAndRole()
    {
        var book = BookFakes.MakeBook();
        book.AddAuthor(1, "Author");
        book.AddAuthor(1, "Author");
        book.Authors.Should().HaveCount(1);
    }

    [Fact]
    public void RemoveAuthor_RemovesFromCollection()
    {
        var book = BookFakes.MakeBook();
        book.AddAuthor(1, "Author");
        book.RemoveAuthor(1, "Author");
        book.Authors.Should().BeEmpty();
    }

    [Fact]
    public void AddGenre_AddsGenreToCollection()
    {
        var book = BookFakes.MakeBook();
        book.AddGenre(5);
        book.Genres.Should().HaveCount(1);
    }

    [Fact]
    public void AddGenre_IsIdempotent_ForSameGenre()
    {
        var book = BookFakes.MakeBook();
        book.AddGenre(5);
        book.AddGenre(5);
        book.Genres.Should().HaveCount(1);
    }

    [Fact]
    public void AddCopy_AddsCopyToCollection()
    {
        var book = BookFakes.MakeBook(id: 1);
        book.AddCopy(99, "BC-001", 1, 1, "A1-01", BookCondition.Good, DateTime.Today);
        book.TotalCopies.Should().Be(1);
    }

    [Fact]
    public void AddCopy_Throws_OnDuplicateBarcode()
    {
        var book = BookFakes.MakeBook(id: 1);
        book.AddCopy(99, "BC-001", 1, 1, "A1-01", BookCondition.Good, DateTime.Today);
        var act = () => book.AddCopy(100, "BC-001", 1, 1, "A1-02", BookCondition.Good, DateTime.Today);
        act.Should().Throw<InvalidOperationException>().WithMessage("*BC-001*");
    }

    [Fact]
    public void UpdateDetails_Throws_WhenPagesIsNegative()
    {
        var book = BookFakes.MakeBook();
        var act  = () => book.UpdateDetails(null, -1, null, null, null, null, null);
        act.Should().Throw<ArgumentException>().WithMessage("*Pages*");
    }

    [Fact]
    public void UpdateDetails_Throws_WhenCopyrightYearIsInvalid()
    {
        var book = BookFakes.MakeBook();
        var act  = () => book.UpdateDetails(null, null, null, null, 999, null, null);
        act.Should().Throw<ArgumentException>().WithMessage("*Copyright*");
    }
}