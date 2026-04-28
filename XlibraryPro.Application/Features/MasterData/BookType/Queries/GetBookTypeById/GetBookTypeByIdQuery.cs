using MediatR;
using XlibraryPro.Application.Features.MasterData.BookType.Dto;

namespace XlibraryPro.Application.Features.MasterData.BookType.Queries.GetBookTypeById;

public record GetBookTypeByIdQuery(long Id) : IRequest<BookTypeDto?>;
