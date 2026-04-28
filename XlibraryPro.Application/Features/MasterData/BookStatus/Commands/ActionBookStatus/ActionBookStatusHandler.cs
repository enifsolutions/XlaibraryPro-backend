using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.BookStatus.Commands.ActionBookStatus;

public class ActionBookStatusHandler(IBookStatusRepository repo) : IRequestHandler<ActionBookStatusCommand, int>
{
    public async Task<int> Handle(ActionBookStatusCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.BookStatus, cmd.VAction, ct);
}
