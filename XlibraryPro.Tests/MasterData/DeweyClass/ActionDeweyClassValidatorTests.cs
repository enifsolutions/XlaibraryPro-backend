using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Commands.ActionDeweyClass;

namespace XlibraryPro.Tests.MasterData.DeweyClass;

public class ActionDeweyClassValidatorTests
{
    private readonly ActionDeweyClassValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionDeweyClassCommand(0, "ADD", "000", "Computer Science"));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyNumber_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionDeweyClassCommand(0, "ADD", "", "Computer Science"));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_EmptyCaption_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionDeweyClassCommand(0, "ADD", "000", ""));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_NumberTooLong_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionDeweyClassCommand(0, "ADD", new string('0', 21), "Caption"));
        result.IsValid.Should().BeFalse();
    }
}