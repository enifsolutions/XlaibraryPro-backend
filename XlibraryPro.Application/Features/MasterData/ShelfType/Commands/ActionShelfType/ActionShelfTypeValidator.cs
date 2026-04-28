using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.ShelfType.Commands.ActionShelfType;

public class ActionShelfTypeValidator : AbstractValidator<ActionShelfTypeCommand>
{
    public ActionShelfTypeValidator()
    {
        RuleFor(x => x.VAction)
            .NotEmpty()
            .Must(v => v == "ADD" || v == "UPDATE")
            .WithMessage("VAction must be ADD or UPDATE");

        RuleFor(x => x.ShelfType)
            .NotEmpty()
            .MaximumLength(100);
    }
}
