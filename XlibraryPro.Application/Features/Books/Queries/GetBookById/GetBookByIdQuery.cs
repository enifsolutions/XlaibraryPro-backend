using MediatR;
using XlibraryPro.Application.Features.Books.DTOs;

namespace XlibraryPro.Application.Features.Books.Queries.GetBookById;

public record GetBookByIdQuery(long Id) : IRequest<BookDetailDto?>;