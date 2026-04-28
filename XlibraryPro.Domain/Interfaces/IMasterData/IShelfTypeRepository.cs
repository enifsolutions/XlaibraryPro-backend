using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IShelfTypeRepository
{
    Task<IEnumerable<ShelfTypeModel>> GetAllAsync(CancellationToken ct = default);
    Task<ShelfTypeModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string shelfType, string vAction, CancellationToken ct = default);
}
