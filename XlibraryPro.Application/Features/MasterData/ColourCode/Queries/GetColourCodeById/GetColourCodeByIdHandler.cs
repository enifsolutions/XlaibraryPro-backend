using MediatR;
using XlibraryPro.Application.Features.MasterData.ColourCode.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.ColourCode.Queries.GetColourCodeById;

public class GetColourCodeByIdHandler(IColourCodeRepository repo) : IRequestHandler<GetColourCodeByIdQuery, ColourCodeDto?>
{
    public async Task<ColourCodeDto?> Handle(GetColourCodeByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : ColourCodeDto.FromModel(model);
    }
}
