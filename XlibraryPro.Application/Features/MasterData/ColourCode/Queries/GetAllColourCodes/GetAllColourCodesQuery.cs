using MediatR;
using XlibraryPro.Application.Features.MasterData.ColourCode.Dto;

namespace XlibraryPro.Application.Features.MasterData.ColourCode.Queries.GetAllColourCodes;

public record GetAllColourCodesQuery : IRequest<IEnumerable<ColourCodeDto>>;
