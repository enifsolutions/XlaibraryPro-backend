using MediatR;
using XlibraryPro.Application.Features.MasterData.ShelfType.Dto;

namespace XlibraryPro.Application.Features.MasterData.ShelfType.Queries.GetShelfTypeById;

public record GetShelfTypeByIdQuery(long Id) : IRequest<ShelfTypeDto?>;
