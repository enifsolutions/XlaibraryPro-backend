using MediatR;
using XlibraryPro.Application.Features.MasterData.ColourCode.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.ColourCode.Queries.GetAllColourCodes;

public class GetAllColourCodesHandler(IColourCodeRepository repo) : IRequestHandler<GetAllColourCodesQuery, IEnumerable<ColourCodeDto>>
{
    public async Task<IEnumerable<ColourCodeDto>> Handle(GetAllColourCodesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(ColourCodeDto.FromModel);
}
