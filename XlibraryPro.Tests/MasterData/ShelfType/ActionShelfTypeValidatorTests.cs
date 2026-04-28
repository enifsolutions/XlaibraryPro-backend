using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.ShelfType.Commands.ActionShelfType;

namespace XlibraryPro.Tests.MasterData.ShelfType;

public class ActionShelfTypeValidatorTests
{
    private readonly ActionShelfTypeValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionShelfTypeCommand(0, "ADD", "Restricted"));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyShelfType_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionShelfTypeCommand(0, "ADD", ""));
        result.IsValid.Should().BeFalse();
    }
}