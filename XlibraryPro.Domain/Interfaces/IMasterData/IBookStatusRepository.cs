using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IBookStatusRepository
{
    Task<IEnumerable<BookStatusModel>> GetAllAsync(CancellationToken ct = default);
    Task<BookStatusModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string bookStatus, string vAction, CancellationToken ct = default);
}
