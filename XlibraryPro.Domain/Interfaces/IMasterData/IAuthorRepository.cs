using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IAuthorRepository
{
    Task<IEnumerable<AuthorModel>> GetAllAsync(CancellationToken ct = default);
    Task<AuthorModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string firstName, string? middleName, string lastName, string? dates, string? notes, string vAction, CancellationToken ct = default);
}
