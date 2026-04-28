using FluentAssertions;
using XlibraryPro.Application.Features.Books.Commands.UpdateBook;

namespace XlibraryPro.Tests.Books.Validators;

public class UpdateBookValidatorTests
{
    private readonly UpdateBookValidator _validator = new();

    private static UpdateBookCommand ValidCommand() => new(
        Id:                1,
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

    [Fact]
    public async Task Validate_Fails_WhenIdIsZero()
    {
        var cmd = ValidCommand() with { Id = 0 };
        var result = await _validator.ValidateAsync(cmd);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id");
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
    public async Task Validate_Fails_WhenDeweyIdIsZero()
    {
        var cmd = ValidCommand() with { DeweyId = 0 };
        var result = await _validator.ValidateAsync(cmd);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "DeweyId");
    }
}