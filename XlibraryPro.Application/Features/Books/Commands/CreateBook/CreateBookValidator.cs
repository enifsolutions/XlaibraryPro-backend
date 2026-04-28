using FluentValidation;

namespace XlibraryPro.Application.Features.Books.Commands.CreateBook;

public class CreateBookValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255);

        RuleFor(x => x.PrimaryLanguageId).GreaterThan(0).WithMessage("Language is required.");
        RuleFor(x => x.BookTypeId).GreaterThan(0).WithMessage("Book type is required.");
        RuleFor(x => x.DeweyId).GreaterThan(0).WithMessage("Dewey class is required.");
        RuleFor(x => x.PublisherId).GreaterThan(0).WithMessage("Publisher is required.");

        RuleFor(x => x.Pages)
            .GreaterThan(0).When(x => x.Pages.HasValue)
            .WithMessage("Pages must be greater than 0.");

        RuleFor(x => x.CopyrightYear)
            .InclusiveBetween(1000, 9999).When(x => x.CopyrightYear.HasValue)
            .WithMessage("Copyright year must be a valid 4-digit year.");
    }
}