using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.StudentBatch.Commands.ActionStudentBatch;

public class ActionStudentBatchHandler(IStudentBatchRepository repo) : IRequestHandler<ActionStudentBatchCommand, int>
{
    public async Task<int> Handle(ActionStudentBatchCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.SchoolYear, cmd.ColourCodeId, cmd.MaxBooksAllowed, cmd.VAction, ct);
}
