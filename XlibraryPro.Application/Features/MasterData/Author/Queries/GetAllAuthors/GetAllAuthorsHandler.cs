using MediatR;
using XlibraryPro.Application.Features.MasterData.Author.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Author.Queries.GetAllAuthors;

public class GetAllAuthorsHandler(IAuthorRepository repo) : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
{
    public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(AuthorDto.FromModel);
}
