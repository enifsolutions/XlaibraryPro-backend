using MediatR;

namespace XlibraryPro.Application.Features.MasterData.LoanStatus.Commands.ActionLoanStatus;

public record ActionLoanStatusCommand(
    long   Id,
    string VAction,
    string LoanStatus
) : IRequest<int>;
