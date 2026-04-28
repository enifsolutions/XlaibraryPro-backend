using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.DeweyClass.Commands.ActionDeweyClass;

public class ActionDeweyClassValidator : AbstractValidator<ActionDeweyClassCommand>
{
    public ActionDeweyClassValidator()
    {
        RuleFor(x => x.VAction).NotEmpty().Must(v => v == "ADD" || v == "UPDATE").WithMessage("VAction must be ADD or UPDATE");
        RuleFor(x => x.DeweyNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.DeweyCaption).NotEmpty().MaximumLength(200);
    }
}
