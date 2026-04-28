using MediatR;

namespace XlibraryPro.Application.Features.MasterData.MemberStatus.Commands.ActionMemberStatus;

public record ActionMemberStatusCommand(
    long   Id,
    string VAction,
    string Status
) : IRequest<int>;
