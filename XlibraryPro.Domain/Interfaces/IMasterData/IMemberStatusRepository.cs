using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IMemberStatusRepository
{
    Task<IEnumerable<MemberStatusModel>> GetAllAsync(CancellationToken ct = default);
    Task<MemberStatusModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string status, string vAction, CancellationToken ct = default);
}
