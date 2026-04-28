using MediatR;
using XlibraryPro.Application.Features.MasterData.ShelfType.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.ShelfType.Queries.GetAllShelfTypes;

public class GetAllShelfTypesHandler(IShelfTypeRepository repo) : IRequestHandler<GetAllShelfTypesQuery, IEnumerable<ShelfTypeDto>>
{
    public async Task<IEnumerable<ShelfTypeDto>> Handle(GetAllShelfTypesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(ShelfTypeDto.FromModel);
}
