using MediatR;
using XlibraryPro.Application.Features.Books.DTOs;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Application.Features.Books.Queries.GetBookById;

public class GetBookByIdHandler(IBookRepository repo) : IRequestHandler<GetBookByIdQuery, BookDetailDto?>
{
    public async Task<BookDetailDto?> Handle(GetBookByIdQuery request, CancellationToken ct)
    {
        var book = await repo.GetByIdAsync(request.Id, ct);
        if (book is null) return null;

        return new BookDetailDto
        {
            BookId             = book.Id,
            Title              = book.Title,
            Description        = book.Description,
            Isbn               = book.Isbn?.Value,
            Pages              = book.Pages,
            PrimaryLanguageId  = book.PrimaryLanguageId,
            BookTypeId         = book.BookTypeId,
            DeweyId            = book.DeweyId,
            PublisherId        = book.PublisherId,
            PlaceOfPublication = book.PlaceOfPublication,
            PublicationYear    = book.PublicationYear,
            CopyrightYear      = book.CopyrightYear,
            EditionStatement   = book.EditionStatement,
            Notes              = book.Notes
        };
    }
}