using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.Language.Commands.ActionLanguage;

public class ActionLanguageValidator : AbstractValidator<ActionLanguageCommand>
{
    public ActionLanguageValidator()
    {
        RuleFor(x => x.VAction)
            .NotEmpty()
            .Must(v => v == "ADD" || v == "UPDATE")
            .WithMessage("VAction must be ADD or UPDATE");

        RuleFor(x => x.Language)
            .NotEmpty()
            .MaximumLength(100);
    }
}
