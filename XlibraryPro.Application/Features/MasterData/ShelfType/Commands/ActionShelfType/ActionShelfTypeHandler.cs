using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.ShelfType.Commands.ActionShelfType;

public class ActionShelfTypeHandler(IShelfTypeRepository repo) : IRequestHandler<ActionShelfTypeCommand, int>
{
    public async Task<int> Handle(ActionShelfTypeCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.ShelfType, cmd.VAction, ct);
}
