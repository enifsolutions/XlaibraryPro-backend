using MediatR;

namespace XlibraryPro.Application.Features.MasterData.DeweyClass.Commands.ActionDeweyClass;

public record ActionDeweyClassCommand(
    long   Id,
    string VAction,
    string DeweyNumber,
    string DeweyCaption
) : IRequest<int>;
