using MediatR;
using XlibraryPro.Application.Features.MasterData.Publisher.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Publisher.Queries.GetAllPublishers;

public class GetAllPublishersHandler(IPublisherRepository repo) : IRequestHandler<GetAllPublishersQuery, IEnumerable<PublisherDto>>
{
    public async Task<IEnumerable<PublisherDto>> Handle(GetAllPublishersQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(PublisherDto.FromModel);
}
