using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.LoanStatus.Dto;

public class LoanStatusDto
{
    public long   LoanStatusId   { get; set; }
    public string LoanStatus { get; set; } = string.Empty;

    public static LoanStatusDto FromModel(LoanStatusModel m) => new()
    {
        LoanStatusId   = m.LoanStatusId,
        LoanStatus = m.LoanStatus
    };
}
