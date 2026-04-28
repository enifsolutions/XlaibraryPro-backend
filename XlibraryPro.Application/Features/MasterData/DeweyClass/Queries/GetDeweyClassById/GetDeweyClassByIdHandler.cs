using MediatR;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.DeweyClass.Queries.GetDeweyClassById;

public class GetDeweyClassByIdHandler(IDeweyClassRepository repo) : IRequestHandler<GetDeweyClassByIdQuery, DeweyClassDto?>
{
    public async Task<DeweyClassDto?> Handle(GetDeweyClassByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : DeweyClassDto.FromModel(model);
    }
}
