using MediatR;
using XlibraryPro.Application.Features.MasterData.Publisher.Dto;

namespace XlibraryPro.Application.Features.MasterData.Publisher.Queries.GetPublisherById;

public record GetPublisherByIdQuery(long Id) : IRequest<PublisherDto?>;
