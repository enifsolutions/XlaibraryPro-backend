using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IStudentBatchRepository
{
    Task<IEnumerable<StudentBatchModel>> GetAllAsync(CancellationToken ct = default);
    Task<StudentBatchModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string schoolYear, long colourCodeId, int maxBooksAllowed, string vAction, CancellationToken ct = default);
}
