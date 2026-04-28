using FluentValidation;

namespace XlibraryPro.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Valid book ID is required.");
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.PrimaryLanguageId).GreaterThan(0);
        RuleFor(x => x.BookTypeId).GreaterThan(0);
        RuleFor(x => x.DeweyId).GreaterThan(0);
        RuleFor(x => x.PublisherId).GreaterThan(0);
        RuleFor(x => x.Pages).GreaterThan(0).When(x => x.Pages.HasValue);
        RuleFor(x => x.CopyrightYear).InclusiveBetween(1000, 9999).When(x => x.CopyrightYear.HasValue);
    }
}