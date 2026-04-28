using MediatR;

namespace XlibraryPro.Application.Features.MasterData.ColourCode.Commands.ActionColourCode;

public record ActionColourCodeCommand(
    long   Id,
    string VAction,
    string Colour,
    int    RotationalOrder
) : IRequest<int>;
