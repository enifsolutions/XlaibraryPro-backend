using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.Author.Commands.ActionAuthor;

namespace XlibraryPro.Tests.MasterData.Author;

public class ActionAuthorValidatorTests
{
    private readonly ActionAuthorValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionAuthorCommand(0, "ADD", "Arthur", null, "Clarke", "1917-2008", null));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyFirstName_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionAuthorCommand(0, "ADD", "", null, "Clarke", null, null));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_EmptyLastName_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionAuthorCommand(0, "ADD", "Arthur", null, "", null, null));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_FirstNameTooLong_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionAuthorCommand(0, "ADD", new string('x', 101), null, "Clarke", null, null));
        result.IsValid.Should().BeFalse();
    }
}