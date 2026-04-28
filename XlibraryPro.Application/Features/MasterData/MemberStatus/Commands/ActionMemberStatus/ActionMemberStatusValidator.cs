using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.MemberStatus.Commands.ActionMemberStatus;

public class ActionMemberStatusValidator : AbstractValidator<ActionMemberStatusCommand>
{
    public ActionMemberStatusValidator()
    {
        RuleFor(x => x.VAction)
            .NotEmpty()
            .Must(v => v == "ADD" || v == "UPDATE")
            .WithMessage("VAction must be ADD or UPDATE");

        RuleFor(x => x.Status)
            .NotEmpty()
            .MaximumLength(100);
    }
}
