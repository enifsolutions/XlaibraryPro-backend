using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.Language.Commands.ActionLanguage;

namespace XlibraryPro.Tests.MasterData.Language;

public class ActionLanguageValidatorTests
{
    private readonly ActionLanguageValidator _sut = new();

    [Theory]
    [InlineData("ADD")]
    [InlineData("UPDATE")]
    public async Task Validate_ValidAction_Passes(string action)
    {
        var result = await _sut.ValidateAsync(new ActionLanguageCommand(0, action, "English"));
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("DELETE")]
    [InlineData("")]
    [InlineData("add")]
    public async Task Validate_InvalidAction_Fails(string action)
    {
        var result = await _sut.ValidateAsync(new ActionLanguageCommand(0, action, "English"));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_EmptyLanguage_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionLanguageCommand(0, "ADD", ""));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_LanguageTooLong_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionLanguageCommand(0, "ADD", new string('x', 101)));
        result.IsValid.Should().BeFalse();
    }
}