using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface ILoanStatusRepository
{
    Task<IEnumerable<LoanStatusModel>> GetAllAsync(CancellationToken ct = default);
    Task<LoanStatusModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string loanStatus, string vAction, CancellationToken ct = default);
}
