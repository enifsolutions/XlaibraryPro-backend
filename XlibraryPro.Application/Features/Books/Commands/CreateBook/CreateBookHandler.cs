using System.Diagnostics.CodeAnalysis;
using MediatR;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Application.Features.Books.Commands.CreateBook;

public class CreateBookHandler(IBookRepository repo) : IRequestHandler<CreateBookCommand, long>
{
    public async Task<long> Handle(CreateBookCommand cmd, CancellationToken ct)
    {
        var book = new Book(
            id: 0,
            title: cmd.Title,
            primaryLanguageId: cmd.PrimaryLanguageId,
            bookTypeId: cmd.BookTypeId,
            deweyId: cmd.DeweyId,
            publisherId: cmd.PublisherId,
            isbn: cmd.Isbn,
            description: cmd.Description,
            pages: cmd.Pages,
            placeOfPublication: cmd.PlaceOfPublication,
            publicationYear: cmd.PublicationYear,
            copyrightYear: cmd.CopyrightYear,
            editionStatement: cmd.EditionStatement,
            notes: cmd.Notes
        );
        book.CoverImageUrl = cmd.CoverImageUrl;

        var newId = await repo.AddAsync(book, ct);

        if (cmd.AuthorIds != null && cmd.AuthorIds.Any())
            await repo.SyncAuthorsAsync(newId, cmd.AuthorIds, ct);

        if (cmd.GenreIds != null && cmd.GenreIds.Any())
            await repo.SyncGenresAsync(newId, cmd.GenreIds, ct);

        return newId;
    }
}