using MediatR;
using XlibraryPro.Application.Features.MasterData.Publisher.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Publisher.Queries.GetPublisherById;

public class GetPublisherByIdHandler(IPublisherRepository repo) : IRequestHandler<GetPublisherByIdQuery, PublisherDto?>
{
    public async Task<PublisherDto?> Handle(GetPublisherByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : PublisherDto.FromModel(model);
    }
}
