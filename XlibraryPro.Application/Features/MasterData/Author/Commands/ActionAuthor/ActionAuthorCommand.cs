using MediatR;

namespace XlibraryPro.Application.Features.MasterData.Author.Commands.ActionAuthor;

public record ActionAuthorCommand(
    long    Id,
    string  VAction,
    string  FirstName,
    string? MiddleName,
    string  LastName,
    string? Dates,
    string? Notes
) : IRequest<int>;
