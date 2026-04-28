using MediatR;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Dto;

namespace XlibraryPro.Application.Features.MasterData.StudentBatch.Queries.GetStudentBatchById;

public record GetStudentBatchByIdQuery(long Id) : IRequest<StudentBatchDto?>;
