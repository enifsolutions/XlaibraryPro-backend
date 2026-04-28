using MediatR;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.MemberStatus.Queries.GetMemberStatusById;

public class GetMemberStatusByIdHandler(IMemberStatusRepository repo) : IRequestHandler<GetMemberStatusByIdQuery, MemberStatusDto?>
{
    public async Task<MemberStatusDto?> Handle(GetMemberStatusByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : MemberStatusDto.FromModel(model);
    }
}
