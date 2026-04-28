using MediatR;
using XlibraryPro.Application.Features.MasterData.Language.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Language.Queries.GetLanguageById;

public class GetLanguageByIdHandler(ILanguageRepository repo) : IRequestHandler<GetLanguageByIdQuery, LanguageDto?>
{
    public async Task<LanguageDto?> Handle(GetLanguageByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : LanguageDto.FromModel(model);
    }
}
