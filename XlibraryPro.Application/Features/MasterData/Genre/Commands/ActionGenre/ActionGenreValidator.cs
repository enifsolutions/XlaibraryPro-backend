using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.Genre.Commands.ActionGenre;

public class ActionGenreValidator : AbstractValidator<ActionGenreCommand>
{
    public ActionGenreValidator()
    {
        RuleFor(x => x.VAction)
            .NotEmpty()
            .Must(v => v == "ADD" || v == "UPDATE")
            .WithMessage("VAction must be ADD or UPDATE");

        RuleFor(x => x.GenreFormName)
            .NotEmpty()
            .MaximumLength(150);
    }
}
