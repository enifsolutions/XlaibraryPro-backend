using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Domain.Interfaces.IMasterData;

public interface IPublisherRepository
{
    Task<IEnumerable<PublisherModel>> GetAllAsync(CancellationToken ct = default);
    Task<PublisherModel?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<int> ActionAsync(long id, string publisherName, string vAction, CancellationToken ct = default);
}
