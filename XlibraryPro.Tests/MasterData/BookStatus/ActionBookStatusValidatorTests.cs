using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.BookStatus.Commands.ActionBookStatus;

namespace XlibraryPro.Tests.MasterData.BookStatus;

public class ActionBookStatusValidatorTests
{
    private readonly ActionBookStatusValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionBookStatusCommand(0, "ADD", "Lost"));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyStatus_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionBookStatusCommand(0, "ADD", ""));
        result.IsValid.Should().BeFalse();
    }
}