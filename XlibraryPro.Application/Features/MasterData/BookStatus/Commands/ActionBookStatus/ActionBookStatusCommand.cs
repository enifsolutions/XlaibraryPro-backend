using MediatR;

namespace XlibraryPro.Application.Features.MasterData.BookStatus.Commands.ActionBookStatus;

public record ActionBookStatusCommand(
    long   Id,
    string VAction,
    string BookStatus
) : IRequest<int>;
