using MediatR;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.StudentBatch.Queries.GetAllStudentBatches;

public class GetAllStudentBatchesHandler(IStudentBatchRepository repo) : IRequestHandler<GetAllStudentBatchesQuery, IEnumerable<StudentBatchDto>>
{
    public async Task<IEnumerable<StudentBatchDto>> Handle(GetAllStudentBatchesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(StudentBatchDto.FromModel);
}
