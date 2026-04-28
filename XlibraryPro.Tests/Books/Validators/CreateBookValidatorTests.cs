using FluentAssertions;
using XlibraryPro.Application.Features.Books.Commands.CreateBook;

namespace XlibraryPro.Tests.Books.Validators;

public class CreateBookValidatorTests
{
    private readonly CreateBookValidator _validator = new();

    private static CreateBookCommand ValidCommand() => new(
        Title:             "Valid Title",
        PrimaryLanguageId: 1,
        BookTypeId:        1,
        DeweyId:           1,
        PublisherId:       1
    );

    [Fact]
    public async Task Validate_Passes_ForValidCommand()
    {
        var result = await _validator.ValidateAsync(ValidCommand());
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Validate_Fails_WhenTitleIsEmpty(string title)
    {
        var cmd = ValidCommand() with { Title = title };
        var result = await _validator.ValidateAsync(cmd);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Title");
    }

    [Fact]
    public async Task Validate_Fails_WhenLanguageIdIsZero()
    {
        var cmd = ValidCommand() with { PrimaryLanguageId = 0 };
        var result = await _validator.ValidateAsync(cmd);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PrimaryLanguageId");
    }

    [Fact]
    public async Task Validate_Fails_WhenPublisherIdIsZero()
    {
        var cmd = ValidCommand() with { PublisherId = 0 };
        var result = await _validator.ValidateAsync(cmd);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PublisherId");
    }

    [Fact]
    public async Task Validate_Fails_WhenPagesIsNegative()
    {
        var cmd = ValidCommand() with { Pages = -1 };
        var result = await _validator.ValidateAsync(cmd);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Pages");
    }

    [Theory]
    [InlineData(999)]
    [InlineData(10000)]
    public async Task Validate_Fails_WhenCopyrightYearIsInvalid(int year)
    {
        var cmd = ValidCommand() with { CopyrightYear = year };
        var result = await _validator.ValidateAsync(cmd);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "CopyrightYear");
    }

    [Theory]
    [InlineData(2024)]
    [InlineData(1900)]
    [InlineData(1000)]
    [InlineData(9999)]
    public async Task Validate_Passes_WithValidCopyrightYear(int year)
    {
        var cmd = ValidCommand() with { CopyrightYear = year };
        var result = await _validator.ValidateAsync(cmd);
        result.Errors.Should().NotContain(e => e.PropertyName == "CopyrightYear");
    }

    [Fact]
    public async Task Validate_Passes_WithNullOptionalFields()
    {
        var cmd = ValidCommand() with
        {
            Isbn              = null,
            Description       = null,
            Pages             = null,
            CopyrightYear     = null,
            EditionStatement  = null,
            Notes             = null
        };
        var result = await _validator.ValidateAsync(cmd);
        result.IsValid.Should().BeTrue();
    }
}