using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.BookType.Commands.ActionBookType;

public class ActionBookTypeHandler(IBookTypeRepository repo) : IRequestHandler<ActionBookTypeCommand, int>
{
    public async Task<int> Handle(ActionBookTypeCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.BookType, cmd.VAction, ct);
}
