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

        var authors = await repo.GetAuthorsAsync(request.Id, ct);
        var genres = await repo.GetGenresAsync(request.Id, ct);

        return new BookDetailDto
        {
            BookId = book.Id,
            Title = book.Title,
            Description = book.Description,
            Isbn = book.Isbn,
            Pages = book.Pages,
            PrimaryLanguageId = book.PrimaryLanguageId,
            LanguageName = book.LanguageName,
            BookTypeId = book.BookTypeId,
            BookType = book.BookTypeName,
            DeweyId = book.DeweyId,
            DeweyNumber = book.DeweyNumber,
            DeweyCaption = book.DeweyCaption,
            PublisherId = book.PublisherId,
            PublisherName = book.PublisherName,
            PlaceOfPublication = book.PlaceOfPublication,
            PublicationYear = book.PublicationYear,
            CopyrightYear = book.CopyrightYear,
            EditionStatement = book.EditionStatement,
            Notes = book.Notes,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies,
            CoverImageUrl = book.CoverImageUrl,
            Authors = authors.Select(a => new BookAuthorDto
            {
                AuthorId = a.AuthorId,
                FirstName = a.FirstName,
                MiddleName = a.MiddleName,
                LastName = a.LastName,
                Role = a.Role,
            }).ToList(),
            Genres = genres.Select(g => new BookGenreDto
            {
                GenreFormId = g.GenreFormId,
                GenreFormName = g.GenreFormName,
            }).ToList(),
        };
    }
}