using FluentAssertions;
using XlibraryPro.Domain.ValueObjects;

namespace XlibraryPro.Tests.Books.Domain;

public class IsbnValueObjectTests
{
    [Theory]
    [InlineData("9780132350884")]   // ISBN-13 valid
    [InlineData("9780134494166")]   // ISBN-13 valid
    [InlineData("978-0-13-235088-4")] // ISBN-13 with dashes
    public void Create_Succeeds_ForValidIsbn13(string isbn)
    {
        var result = Isbn.Create(isbn);
        result.Should().NotBeNull();
        result.Value.Should().NotContain("-");
    }

    [Theory]
    [InlineData("0132350882")]     // ISBN-10 valid
    [InlineData("0306406152")]     // ISBN-10 with X check digit
    public void Create_Succeeds_ForValidIsbn10(string isbn)
    {
        var result = Isbn.Create(isbn);
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData("1234567890123")]  // wrong checksum
    [InlineData("000000000000")]   // wrong checksum
    [InlineData("abcdefghijklm")]  // non-numeric
    [InlineData("123")]            // too short
    [InlineData("")]               // empty
    public void Create_Throws_ForInvalidIsbn(string isbn)
    {
        var act = () => Isbn.Create(isbn);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void TryCreate_ReturnsNull_ForInvalidIsbn()
    {
        var result = Isbn.TryCreate("not-an-isbn");
        result.Should().BeNull();
    }

    [Fact]
    public void TryCreate_ReturnsNull_ForNullInput()
    {
        var result = Isbn.TryCreate(null);
        result.Should().BeNull();
    }

    [Fact]
    public void Equals_ReturnsTrue_ForSameValue()
    {
        var a = Isbn.Create("9780132350884");
        var b = Isbn.Create("9780132350884");
        a.Should().Be(b);
    }

    [Fact]
    public void Equals_ReturnsFalse_ForDifferentValue()
    {
        var a = Isbn.Create("9780132350884");
        var b = Isbn.Create("9780134494166");
        a.Should().NotBe(b);
    }

    [Fact]
    public void ToString_ReturnsNormalizedValue()
    {
        var isbn = Isbn.Create("978-0-13-235088-4");
        isbn.ToString().Should().Be("9780132350884");
    }
}
