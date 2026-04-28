using MediatR;
using XlibraryPro.Application.Features.MasterData.BookType.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.BookType.Queries.GetAllBookTypes;

public class GetAllBookTypesHandler(IBookTypeRepository repo) : IRequestHandler<GetAllBookTypesQuery, IEnumerable<BookTypeDto>>
{
    public async Task<IEnumerable<BookTypeDto>> Handle(GetAllBookTypesQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(BookTypeDto.FromModel);
}
