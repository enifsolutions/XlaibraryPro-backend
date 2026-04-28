using MediatR;
using XlibraryPro.Application.Features.Books.DTOs;

namespace XlibraryPro.Application.Features.Books.Queries.SearchBooks;

public record SearchBooksQuery(string Term) : IRequest<IEnumerable<BookDto>>;