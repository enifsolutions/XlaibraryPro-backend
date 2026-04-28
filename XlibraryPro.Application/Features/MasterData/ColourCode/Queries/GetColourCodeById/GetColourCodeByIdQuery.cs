using MediatR;
using XlibraryPro.Application.Features.MasterData.ColourCode.Dto;

namespace XlibraryPro.Application.Features.MasterData.ColourCode.Queries.GetColourCodeById;

public record GetColourCodeByIdQuery(long Id) : IRequest<ColourCodeDto?>;
