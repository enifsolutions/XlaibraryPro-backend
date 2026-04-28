using MediatR;
using XlibraryPro.Application.Features.MasterData.ShelfType.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.ShelfType.Queries.GetShelfTypeById;

public class GetShelfTypeByIdHandler(IShelfTypeRepository repo) : IRequestHandler<GetShelfTypeByIdQuery, ShelfTypeDto?>
{
    public async Task<ShelfTypeDto?> Handle(GetShelfTypeByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : ShelfTypeDto.FromModel(model);
    }
}
