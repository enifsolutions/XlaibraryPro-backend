using MediatR;

namespace XlibraryPro.Application.Features.MasterData.StudentBatch.Commands.ActionStudentBatch;

public record ActionStudentBatchCommand(
    long   Id,
    string VAction,
    string SchoolYear,
    long   ColourCodeId,
    int    MaxBooksAllowed
) : IRequest<int>;
