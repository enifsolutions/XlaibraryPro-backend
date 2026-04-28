using MediatR;
using XlibraryPro.Application.Features.MasterData.BookStatus.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.BookStatus.Queries.GetBookStatusById;

public class GetBookStatusByIdHandler(IBookStatusRepository repo) : IRequestHandler<GetBookStatusByIdQuery, BookStatusDto?>
{
    public async Task<BookStatusDto?> Handle(GetBookStatusByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : BookStatusDto.FromModel(model);
    }
}
