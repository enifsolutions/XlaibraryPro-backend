using MediatR;
using XlibraryPro.Application.Features.MasterData.ShelfType.Dto;

namespace XlibraryPro.Application.Features.MasterData.ShelfType.Queries.GetAllShelfTypes;

public record GetAllShelfTypesQuery : IRequest<IEnumerable<ShelfTypeDto>>;
