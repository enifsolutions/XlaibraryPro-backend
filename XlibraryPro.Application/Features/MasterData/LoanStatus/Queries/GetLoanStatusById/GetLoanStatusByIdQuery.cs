using MediatR;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Dto;

namespace XlibraryPro.Application.Features.MasterData.LoanStatus.Queries.GetLoanStatusById;

public record GetLoanStatusByIdQuery(long Id) : IRequest<LoanStatusDto?>;
