using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.BookType.Commands.ActionBookType;

namespace XlibraryPro.Tests.MasterData.BookType;

public class ActionBookTypeValidatorTests
{
    private readonly ActionBookTypeValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionBookTypeCommand(0, "ADD", "Reference"));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyBookType_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionBookTypeCommand(0, "ADD", ""));
        result.IsValid.Should().BeFalse();
    }
}