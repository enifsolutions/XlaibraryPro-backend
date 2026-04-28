using MediatR;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.LoanStatus.Queries.GetAllLoanStatuses;

public class GetAllLoanStatusesHandler(ILoanStatusRepository repo) : IRequestHandler<GetAllLoanStatusesQuery, IEnumerable<LoanStatusDto>>
{
    public async Task<IEnumerable<LoanStatusDto>> Handle(GetAllLoanStatusesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(LoanStatusDto.FromModel);
}
