using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.MemberStatus.Commands.ActionMemberStatus;

public class ActionMemberStatusHandler(IMemberStatusRepository repo) : IRequestHandler<ActionMemberStatusCommand, int>
{
    public async Task<int> Handle(ActionMemberStatusCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.Status, cmd.VAction, ct);
}
