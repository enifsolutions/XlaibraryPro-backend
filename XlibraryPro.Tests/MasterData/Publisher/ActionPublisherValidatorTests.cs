using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.Publisher.Commands.ActionPublisher;

namespace XlibraryPro.Tests.MasterData.Publisher;

public class ActionPublisherValidatorTests
{
    private readonly ActionPublisherValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionPublisherCommand(0, "ADD", "Oxford University Press"));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyName_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionPublisherCommand(0, "ADD", ""));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_NameTooLong_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionPublisherCommand(0, "ADD", new string('x', 201)));
        result.IsValid.Should().BeFalse();
    }
}