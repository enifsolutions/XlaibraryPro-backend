using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.LoanStatus.Commands.ActionLoanStatus;

public class ActionLoanStatusValidator : AbstractValidator<ActionLoanStatusCommand>
{
    public ActionLoanStatusValidator()
    {
        RuleFor(x => x.VAction)
            .NotEmpty()
            .Must(v => v == "ADD" || v == "UPDATE")
            .WithMessage("VAction must be ADD or UPDATE");

        RuleFor(x => x.LoanStatus)
            .NotEmpty()
            .MaximumLength(100);
    }
}
