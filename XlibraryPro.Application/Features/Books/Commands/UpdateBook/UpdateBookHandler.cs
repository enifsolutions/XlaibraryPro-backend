using MediatR;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookHandler(IBookRepository repo) : IRequestHandler<UpdateBookCommand>
{
    public async Task Handle(UpdateBookCommand cmd, CancellationToken ct)
    {
        var book = new Book(
            id: cmd.Id,
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

        await repo.UpdateAsync(book, ct);

        if (cmd.AuthorIds != null)
            await repo.SyncAuthorsAsync(cmd.Id, cmd.AuthorIds, ct);

        if (cmd.GenreIds != null)
            await repo.SyncGenresAsync(cmd.Id, cmd.GenreIds, ct);
    }
}