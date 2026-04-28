using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IBookTypeRepository
{
    Task<IEnumerable<BookTypeModel>> GetAllAsync(CancellationToken ct = default);
    Task<BookTypeModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string bookType, string vAction, CancellationToken ct = default);
}
