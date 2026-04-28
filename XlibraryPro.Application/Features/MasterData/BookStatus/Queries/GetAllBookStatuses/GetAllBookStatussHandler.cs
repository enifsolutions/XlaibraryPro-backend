using MediatR;
using XlibraryPro.Application.Features.MasterData.BookStatus.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.BookStatus.Queries.GetAllBookStatuses;

public class GetAllBookStatusesHandler(IBookStatusRepository repo) : IRequestHandler<GetAllBookStatusesQuery, IEnumerable<BookStatusDto>>
{
    public async Task<IEnumerable<BookStatusDto>> Handle(GetAllBookStatusesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(BookStatusDto.FromModel);
}
