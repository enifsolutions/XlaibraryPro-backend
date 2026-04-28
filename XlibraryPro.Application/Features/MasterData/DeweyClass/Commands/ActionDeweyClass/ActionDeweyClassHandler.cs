using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.DeweyClass.Commands.ActionDeweyClass;

public class ActionDeweyClassHandler(IDeweyClassRepository repo) : IRequestHandler<ActionDeweyClassCommand, int>
{
    public async Task<int> Handle(ActionDeweyClassCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.DeweyNumber, cmd.DeweyCaption, cmd.VAction, ct);
}
