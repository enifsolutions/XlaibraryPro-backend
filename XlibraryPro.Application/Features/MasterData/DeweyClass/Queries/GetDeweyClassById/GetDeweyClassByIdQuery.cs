using MediatR;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Dto;

namespace XlibraryPro.Application.Features.MasterData.DeweyClass.Queries.GetDeweyClassById;

public record GetDeweyClassByIdQuery(long Id) : IRequest<DeweyClassDto?>;
