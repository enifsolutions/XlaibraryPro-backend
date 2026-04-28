using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IColourCodeRepository
{
    Task<IEnumerable<ColourCodeModel>> GetAllAsync(CancellationToken ct = default);
    Task<ColourCodeModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string colour, int rotationalOrder, string vAction, CancellationToken ct = default);
}
