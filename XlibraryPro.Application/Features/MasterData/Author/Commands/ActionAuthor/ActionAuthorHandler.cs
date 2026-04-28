using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Author.Commands.ActionAuthor;

public class ActionAuthorHandler(IAuthorRepository repo) : IRequestHandler<ActionAuthorCommand, int>
{
    public async Task<int> Handle(ActionAuthorCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.FirstName, cmd.MiddleName, cmd.LastName, cmd.Dates, cmd.Notes, cmd.VAction, ct);
}
