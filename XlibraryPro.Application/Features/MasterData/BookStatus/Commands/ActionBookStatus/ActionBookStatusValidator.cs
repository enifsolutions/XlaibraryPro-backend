using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.BookStatus.Commands.ActionBookStatus;

public class ActionBookStatusValidator : AbstractValidator<ActionBookStatusCommand>
{
    public ActionBookStatusValidator()
    {
        RuleFor(x => x.VAction)
            .NotEmpty()
            .Must(v => v == "ADD" || v == "UPDATE")
            .WithMessage("VAction must be ADD or UPDATE");

        RuleFor(x => x.BookStatus)
            .NotEmpty()
            .MaximumLength(100);
    }
}
