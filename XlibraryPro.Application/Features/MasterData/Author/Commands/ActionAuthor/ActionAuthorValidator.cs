using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.Author.Commands.ActionAuthor;

public class ActionAuthorValidator : AbstractValidator<ActionAuthorCommand>
{
    public ActionAuthorValidator()
    {
        RuleFor(x => x.VAction).NotEmpty().Must(v => v == "ADD" || v == "UPDATE").WithMessage("VAction must be ADD or UPDATE");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.MiddleName).MaximumLength(100).When(x => x.MiddleName is not null);
        RuleFor(x => x.Dates).MaximumLength(50).When(x => x.Dates is not null);
    }
}
