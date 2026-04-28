using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IDeweyClassRepository
{
    Task<IEnumerable<DeweyClassModel>> GetAllAsync(CancellationToken ct = default);
    Task<DeweyClassModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string deweyNumber, string deweyCaption, string vAction, CancellationToken ct = default);
}
