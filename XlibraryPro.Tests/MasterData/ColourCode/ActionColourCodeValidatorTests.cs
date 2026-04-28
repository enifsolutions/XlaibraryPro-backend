using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.ColourCode.Commands.ActionColourCode;

namespace XlibraryPro.Tests.MasterData.ColourCode;

public class ActionColourCodeValidatorTests
{
    private readonly ActionColourCodeValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionColourCodeCommand(0, "ADD", "Green", 3));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyColour_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionColourCodeCommand(0, "ADD", "", 1));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_ZeroRotationalOrder_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionColourCodeCommand(0, "ADD", "Green", 0));
        result.IsValid.Should().BeFalse();
    }
}