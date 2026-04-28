using MediatR;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Dto;

namespace XlibraryPro.Application.Features.MasterData.MemberStatus.Queries.GetMemberStatusById;

public record GetMemberStatusByIdQuery(long Id) : IRequest<MemberStatusDto?>;
