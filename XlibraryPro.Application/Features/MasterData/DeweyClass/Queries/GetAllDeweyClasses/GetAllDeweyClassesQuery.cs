using MediatR;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Dto;

namespace XlibraryPro.Application.Features.MasterData.DeweyClass.Queries.GetAllDeweyClasses;

public record GetAllDeweyClassesQuery : IRequest<IEnumerable<DeweyClassDto>>;
