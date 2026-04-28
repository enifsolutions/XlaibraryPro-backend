using MediatR;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.MemberStatus.Queries.GetAllMemberStatuses;

public class GetAllMemberStatusesHandler(IMemberStatusRepository repo) : IRequestHandler<GetAllMemberStatusesQuery, IEnumerable<MemberStatusDto>>
{
    public async Task<IEnumerable<MemberStatusDto>> Handle(GetAllMemberStatusesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(MemberStatusDto.FromModel);
}
