using MediatR;
using XlibraryPro.Application.Features.Books.DTOs;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Application.Features.Books.Queries.GetBooks;

public class GetBooksHandler(IBookRepository repo) : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken ct)
    {
        var books = await repo.GetAllAsync(ct);
        return books.Select(b => new BookDto
        {
            BookId           = b.Id,
            Title            = b.Title,
            Isbn             = b.Isbn?.Value,
            Pages            = b.Pages,
            PublicationYear  = b.PublicationYear,
            EditionStatement = b.EditionStatement
        });
    }
}