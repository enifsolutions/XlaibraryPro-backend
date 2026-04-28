using MediatR;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Dto;

namespace XlibraryPro.Application.Features.MasterData.LoanStatus.Queries.GetAllLoanStatuses;

public record GetAllLoanStatusesQuery : IRequest<IEnumerable<LoanStatusDto>>;
