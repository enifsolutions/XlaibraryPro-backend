using MediatR;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Dto;

namespace XlibraryPro.Application.Features.MasterData.StudentBatch.Queries.GetAllStudentBatches;

public record GetAllStudentBatchesQuery : IRequest<IEnumerable<StudentBatchDto>>;
