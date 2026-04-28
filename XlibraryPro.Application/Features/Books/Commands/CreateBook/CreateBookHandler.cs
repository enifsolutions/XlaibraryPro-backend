using MediatR;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Application.Features.Books.Commands.CreateBook;

public class CreateBookHandler(IBookRepository repo) : IRequestHandler<CreateBookCommand, long>
{
    public async Task<long> Handle(CreateBookCommand cmd, CancellationToken ct)
    {
        var book = new Book(
            id:                  0,
            title:               cmd.Title,
            primaryLanguageId:   cmd.PrimaryLanguageId,
            bookTypeId:          cmd.BookTypeId,
            deweyId:             cmd.DeweyId,
            publisherId:         cmd.PublisherId,
            isbn:                cmd.Isbn,
            description:         cmd.Description,
            pages:               cmd.Pages,
            placeOfPublication:  cmd.PlaceOfPublication,
            publicationYear:     cmd.PublicationYear,
            copyrightYear:       cmd.CopyrightYear,
            editionStatement:    cmd.EditionStatement,
            notes:               cmd.Notes
        );

        await repo.AddAsync(book, ct);
        return book.Id;
    }
}