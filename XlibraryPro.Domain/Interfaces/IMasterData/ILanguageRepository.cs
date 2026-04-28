using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface ILanguageRepository
{
    Task<IEnumerable<LanguageModel>> GetAllAsync(CancellationToken ct = default);
    Task<LanguageModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string language, string vAction, CancellationToken ct = default);
}
