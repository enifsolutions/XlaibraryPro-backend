using MediatR;
using XlibraryPro.Application.Features.Books.DTOs;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Application.Features.Books.Queries.SearchBooks;

public class SearchBooksHandler(IBookRepository repo) : IRequestHandler<SearchBooksQuery, IEnumerable<BookDto>>
{
    public async Task<IEnumerable<BookDto>> Handle(SearchBooksQuery request, CancellationToken ct)
    {
        var books = await repo.SearchAsync(request.Term, ct);
        return books.Select(b => new BookDto
        {
            BookId          = b.Id,
            Title           = b.Title,
            Isbn            = b.Isbn,
            PublicationYear = b.PublicationYear
        });
    }
}