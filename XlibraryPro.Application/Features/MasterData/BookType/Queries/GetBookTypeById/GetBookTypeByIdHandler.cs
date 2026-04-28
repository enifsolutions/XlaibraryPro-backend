using MediatR;
using XlibraryPro.Application.Features.MasterData.BookType.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.BookType.Queries.GetBookTypeById;

public class GetBookTypeByIdHandler(IBookTypeRepository repo) : IRequestHandler<GetBookTypeByIdQuery, BookTypeDto?>
{
    public async Task<BookTypeDto?> Handle(GetBookTypeByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : BookTypeDto.FromModel(model);
    }
}
