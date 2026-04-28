using MediatR;

namespace XlibraryPro.Application.Features.Books.Commands.DeleteBook;

public record DeleteBookCommand(long Id) : IRequest;