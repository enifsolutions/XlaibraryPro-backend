using MediatR;

namespace XlibraryPro.Application.Features.MasterData.Genre.Commands.ActionGenre;

public record ActionGenreCommand(
    long   Id,
    string VAction,
    string GenreFormName
) : IRequest<int>;
