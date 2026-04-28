using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Genre.Commands.ActionGenre;

public class ActionGenreHandler(IGenreRepository repo) : IRequestHandler<ActionGenreCommand, int>
{
    public async Task<int> Handle(ActionGenreCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.GenreFormName, cmd.VAction, ct);
}
