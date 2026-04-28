using MediatR;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Application.Features.Books.Commands.DeleteBook;

public class DeleteBookHandler(IBookRepository repo) : IRequestHandler<DeleteBookCommand>
{
    public async Task Handle(DeleteBookCommand request, CancellationToken ct)
    {
        await repo.DeleteAsync(request.Id, ct);
    }
}