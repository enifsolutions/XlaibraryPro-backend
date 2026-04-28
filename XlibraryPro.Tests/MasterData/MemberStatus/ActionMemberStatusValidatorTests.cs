using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Commands.ActionMemberStatus;

namespace XlibraryPro.Tests.MasterData.MemberStatus;

public class ActionMemberStatusValidatorTests
{
    private readonly ActionMemberStatusValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionMemberStatusCommand(0, "ADD", "Graduated"));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyStatus_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionMemberStatusCommand(0, "ADD", ""));
        result.IsValid.Should().BeFalse();
    }
}