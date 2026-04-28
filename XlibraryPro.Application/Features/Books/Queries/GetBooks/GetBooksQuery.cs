using MediatR;
using XlibraryPro.Application.Features.Books.DTOs;

namespace XlibraryPro.Application.Features.Books.Queries.GetBooks;

public record GetBooksQuery : IRequest<IEnumerable<BookDto>>;