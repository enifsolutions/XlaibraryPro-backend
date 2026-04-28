using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.ColourCode.Commands.ActionColourCode;

public class ActionColourCodeValidator : AbstractValidator<ActionColourCodeCommand>
{
    public ActionColourCodeValidator()
    {
        RuleFor(x => x.VAction).NotEmpty().Must(v => v == "ADD" || v == "UPDATE").WithMessage("VAction must be ADD or UPDATE");
        RuleFor(x => x.Colour).NotEmpty().MaximumLength(100);
        RuleFor(x => x.RotationalOrder).GreaterThan(0);
    }
}
