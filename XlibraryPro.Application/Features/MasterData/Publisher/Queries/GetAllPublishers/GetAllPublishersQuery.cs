using MediatR;
using XlibraryPro.Application.Features.MasterData.Publisher.Dto;

namespace XlibraryPro.Application.Features.MasterData.Publisher.Queries.GetAllPublishers;

public record GetAllPublishersQuery : IRequest<IEnumerable<PublisherDto>>;
