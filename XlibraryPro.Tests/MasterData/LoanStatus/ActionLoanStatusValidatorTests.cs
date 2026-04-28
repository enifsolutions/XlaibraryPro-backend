using FluentAssertions;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Commands.ActionLoanStatus;

namespace XlibraryPro.Tests.MasterData.LoanStatus;

public class ActionLoanStatusValidatorTests
{
    private readonly ActionLoanStatusValidator _sut = new();

    [Fact]
    public async Task Validate_ValidCommand_Passes()
    {
        var result = await _sut.ValidateAsync(new ActionLoanStatusCommand(0, "ADD", "Overdue"));
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_EmptyStatus_Fails()
    {
        var result = await _sut.ValidateAsync(new ActionLoanStatusCommand(0, "ADD", ""));
        result.IsValid.Should().BeFalse();
    }
}