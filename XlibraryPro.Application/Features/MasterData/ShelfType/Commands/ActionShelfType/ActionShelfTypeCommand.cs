using MediatR;

namespace XlibraryPro.Application.Features.MasterData.ShelfType.Commands.ActionShelfType;

public record ActionShelfTypeCommand(
    long   Id,
    string VAction,
    string ShelfType
) : IRequest<int>;
