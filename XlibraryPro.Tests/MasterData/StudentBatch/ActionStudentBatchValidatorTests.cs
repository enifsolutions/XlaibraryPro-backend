using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Commands.ActionStudentBatch;

namespace XlibraryPro.Tests.MasterData.StudentBatch;

public class ActionStudentBatchValidatorTests
{
    private readonly ActionStudentBatchValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionStudentBatchCommand(0, "ADD", "2025", 1, 5));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_ZeroColourCodeId_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionStudentBatchCommand(0, "ADD", "2025", 0, 5));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_ZeroMaxBooks_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionStudentBatchCommand(0, "ADD", "2025", 1, 0));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_MaxBooksOver50_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionStudentBatchCommand(0, "ADD", "2025", 1, 51));
        result.IsValid.Should().BeFalse();
    }
}