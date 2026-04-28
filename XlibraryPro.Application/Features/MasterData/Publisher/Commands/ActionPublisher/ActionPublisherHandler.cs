using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Publisher.Commands.ActionPublisher;

public class ActionPublisherHandler(IPublisherRepository repo) : IRequestHandler<ActionPublisherCommand, int>
{
    public async Task<int> Handle(ActionPublisherCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.PublisherName, cmd.VAction, ct);
}
