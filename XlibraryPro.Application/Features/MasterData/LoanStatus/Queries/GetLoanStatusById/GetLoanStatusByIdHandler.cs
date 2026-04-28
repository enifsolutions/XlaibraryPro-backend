using MediatR;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.LoanStatus.Queries.GetLoanStatusById;

public class GetLoanStatusByIdHandler(ILoanStatusRepository repo) : IRequestHandler<GetLoanStatusByIdQuery, LoanStatusDto?>
{
    public async Task<LoanStatusDto?> Handle(GetLoanStatusByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : LoanStatusDto.FromModel(model);
    }
}
