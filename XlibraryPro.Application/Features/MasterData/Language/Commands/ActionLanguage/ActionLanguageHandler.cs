using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Language.Commands.ActionLanguage;

public class ActionLanguageHandler(ILanguageRepository repo) : IRequestHandler<ActionLanguageCommand, int>
{
    public async Task<int> Handle(ActionLanguageCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.Language, cmd.VAction, ct);
}
