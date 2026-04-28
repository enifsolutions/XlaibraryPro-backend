using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.BookType.Commands.ActionBookType;

public class ActionBookTypeValidator : AbstractValidator<ActionBookTypeCommand>
{
    public ActionBookTypeValidator()
    {
        RuleFor(x => x.VAction)
            .NotEmpty()
            .Must(v => v == "ADD" || v == "UPDATE")
            .WithMessage("VAction must be ADD or UPDATE");

        RuleFor(x => x.BookType)
            .NotEmpty()
            .MaximumLength(100);
    }
}
