using MediatR;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.LoanStatus.Commands.ActionLoanStatus;

public class ActionLoanStatusHandler(ILoanStatusRepository repo) : IRequestHandler<ActionLoanStatusCommand, int>
{
    public async Task<int> Handle(ActionLoanStatusCommand cmd, CancellationToken ct)
        => await repo.ActionAsync(cmd.Id, cmd.LoanStatus, cmd.VAction, ct);
}
