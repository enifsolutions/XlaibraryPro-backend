using MediatR;

namespace XlibraryPro.Application.Features.MasterData.BookType.Commands.ActionBookType;

public record ActionBookTypeCommand(
    long   Id,
    string VAction,
    string BookType
) : IRequest<int>;
