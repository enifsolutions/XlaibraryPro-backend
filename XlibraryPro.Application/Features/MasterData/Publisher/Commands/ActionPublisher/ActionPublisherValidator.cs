using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.Publisher.Commands.ActionPublisher;

public class ActionPublisherValidator : AbstractValidator<ActionPublisherCommand>
{
    public ActionPublisherValidator()
    {
        RuleFor(x => x.VAction)
            .NotEmpty()
            .Must(v => v == "ADD" || v == "UPDATE")
            .WithMessage("VAction must be ADD or UPDATE");

        RuleFor(x => x.PublisherName)
            .NotEmpty()
            .MaximumLength(200);
    }
}
