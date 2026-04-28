using FluentValidation;

namespace XlibraryPro.Application.Features.MasterData.StudentBatch.Commands.ActionStudentBatch;

public class ActionStudentBatchValidator : AbstractValidator<ActionStudentBatchCommand>
{
    public ActionStudentBatchValidator()
    {
        RuleFor(x => x.VAction).NotEmpty().Must(v => v == "ADD" || v == "UPDATE").WithMessage("VAction must be ADD or UPDATE");
        RuleFor(x => x.SchoolYear).NotEmpty().MaximumLength(10);
        RuleFor(x => x.ColourCodeId).GreaterThan(0);
        RuleFor(x => x.MaxBooksAllowed).GreaterThan(0).LessThanOrEqualTo(50);
    }
}
