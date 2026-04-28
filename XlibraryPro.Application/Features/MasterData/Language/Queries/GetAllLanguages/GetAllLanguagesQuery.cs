using MediatR;
using XlibraryPro.Application.Features.MasterData.Language.Dto;

namespace XlibraryPro.Application.Features.MasterData.Language.Queries.GetAllLanguages;

public record GetAllLanguagesQuery : IRequest<IEnumerable<LanguageDto>>;
