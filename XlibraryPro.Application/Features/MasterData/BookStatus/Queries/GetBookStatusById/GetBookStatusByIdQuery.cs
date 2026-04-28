using MediatR;
using XlibraryPro.Application.Features.MasterData.BookStatus.Dto;

namespace XlibraryPro.Application.Features.MasterData.BookStatus.Queries.GetBookStatusById;

public record GetBookStatusByIdQuery(long Id) : IRequest<BookStatusDto?>;
