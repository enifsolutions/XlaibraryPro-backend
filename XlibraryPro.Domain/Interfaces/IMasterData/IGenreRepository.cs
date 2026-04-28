using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IGenreRepository
{
    Task<IEnumerable<GenreModel>> GetAllAsync(CancellationToken ct = default);
    Task<GenreModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string genreFormName, string vAction, CancellationToken ct = default);
}
