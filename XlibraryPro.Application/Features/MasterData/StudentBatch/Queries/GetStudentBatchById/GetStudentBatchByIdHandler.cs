using MediatR;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.StudentBatch.Queries.GetStudentBatchById;

public class GetStudentBatchByIdHandler(IStudentBatchRepository repo) : IRequestHandler<GetStudentBatchByIdQuery, StudentBatchDto?>
{
    public async Task<StudentBatchDto?> Handle(GetStudentBatchByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : StudentBatchDto.FromModel(model);
    }
}
