using MediatR;

namespace XlibraryPro.Application.Features.MasterData.Publisher.Commands.ActionPublisher;

public record ActionPublisherCommand(
    long   Id,
    string VAction,
    string PublisherName
) : IRequest<int>;
