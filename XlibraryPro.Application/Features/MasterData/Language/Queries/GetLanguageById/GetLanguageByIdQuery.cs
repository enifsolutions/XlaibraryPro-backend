using MediatR;
using XlibraryPro.Application.Features.MasterData.Language.Dto;

namespace XlibraryPro.Application.Features.MasterData.Language.Queries.GetLanguageById;

public record GetLanguageByIdQuery(long Id) : IRequest<LanguageDto?>;
