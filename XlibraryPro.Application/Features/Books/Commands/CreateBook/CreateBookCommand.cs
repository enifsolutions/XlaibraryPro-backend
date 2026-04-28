using MediatR;

namespace XlibraryPro.Application.Features.Books.Commands.CreateBook;

public record CreateBookCommand(
    string  Title,
    long    PrimaryLanguageId,
    long    BookTypeId,
    long    DeweyId,
    long    PublisherId,
    string? Isbn               = null,
    string? Description        = null,
    int?    Pages              = null,
    string? PlaceOfPublication = null,
    string? PublicationYear    = null,
    int?    CopyrightYear      = null,
    string? EditionStatement   = null,
    string? Notes              = null
) : IRequest<long>;