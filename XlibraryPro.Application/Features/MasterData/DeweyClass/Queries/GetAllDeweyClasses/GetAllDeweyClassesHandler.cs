using MediatR;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.DeweyClass.Queries.GetAllDeweyClasses;

public class GetAllDeweyClassesHandler(IDeweyClassRepository repo) : IRequestHandler<GetAllDeweyClassesQuery, IEnumerable<DeweyClassDto>>
{
    public async Task<IEnumerable<DeweyClassDto>> Handle(GetAllDeweyClassesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(DeweyClassDto.FromModel);
}
