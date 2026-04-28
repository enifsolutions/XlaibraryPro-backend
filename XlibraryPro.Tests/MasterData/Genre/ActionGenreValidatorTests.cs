using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.Genre.Commands.ActionGenre;

namespace XlibraryPro.Tests.MasterData.Genre;

public class ActionGenreValidatorTests
{
    private readonly ActionGenreValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionGenreCommand(0, "ADD", "Science Fiction"));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyName_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionGenreCommand(0, "ADD", ""));
        result.IsValid.Should().BeFalse();
    }
}