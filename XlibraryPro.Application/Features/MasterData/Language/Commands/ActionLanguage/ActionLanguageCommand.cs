using MediatR;

namespace XlibraryPro.Application.Features.MasterData.Language.Commands.ActionLanguage;

public record ActionLanguageCommand(
    long   Id,
    string VAction,
    string Language
) : IRequest<int>;
