using Dapper;
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
            BookId = b.Id,
            Title = b.Title,
            Isbn = b.Isbn,
            Pages = b.Pages,
            PrimaryLanguageId = b.PrimaryLanguageId,
            LanguageName = b.LanguageName,
            BookTypeId = b.BookTypeId,
            BookType = b.BookTypeName,
            DeweyId = b.DeweyId,
            DeweyNumber = b.DeweyNumber,
            DeweyCaption = b.DeweyCaption,
            PublisherId = b.PublisherId,
            PublisherName = b.PublisherName,
            PublicationYear = b.PublicationYear,
            EditionStatement = b.EditionStatement,
            Notes = b.Notes,
            TotalCopies = b.TotalCopies,
            AvailableCopies = b.AvailableCopies,
        });
    }
}