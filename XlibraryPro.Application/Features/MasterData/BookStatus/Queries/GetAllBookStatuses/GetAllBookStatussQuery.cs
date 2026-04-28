using MediatR;
using XlibraryPro.Application.Features.MasterData.BookStatus.Dto;

namespace XlibraryPro.Application.Features.MasterData.BookStatus.Queries.GetAllBookStatuses;

public record GetAllBookStatusesQuery : IRequest<IEnumerable<BookStatusDto>>;
