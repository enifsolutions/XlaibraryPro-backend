using MediatR;
using XlibraryPro.Application.Features.MasterData.Language.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Language.Queries.GetAllLanguages;

public class GetAllLanguagesHandler(ILanguageRepository repo) : IRequestHandler<GetAllLanguagesQuery, IEnumerable<LanguageDto>>
{
    public async Task<IEnumerable<LanguageDto>> Handle(GetAllLanguagesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(LanguageDto.FromModel);
}
