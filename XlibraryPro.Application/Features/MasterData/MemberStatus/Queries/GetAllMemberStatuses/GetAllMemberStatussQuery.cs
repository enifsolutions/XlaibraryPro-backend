using MediatR;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Dto;

namespace XlibraryPro.Application.Features.MasterData.MemberStatus.Queries.GetAllMemberStatuses;

public record GetAllMemberStatusesQuery : IRequest<IEnumerable<MemberStatusDto>>;
