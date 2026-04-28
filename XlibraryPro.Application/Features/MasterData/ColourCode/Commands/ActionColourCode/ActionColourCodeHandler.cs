using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.ColourCode.Commands.ActionColourCode;

public class ActionColourCodeHandler(IColourCodeRepository repo) : IRequestHandler<ActionColourCodeCommand, int>
{
    public async Task<int> Handle(ActionColourCodeCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.Colour, cmd.RotationalOrder, cmd.VAction, ct);
}
