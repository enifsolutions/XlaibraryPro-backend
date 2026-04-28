using MediatR;
using XlibraryPro.Application.Features.MasterData.Author.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Author.Queries.GetAuthorById;

public class GetAuthorByIdHandler(IAuthorRepository repo) : IRequestHandler<GetAuthorByIdQuery, AuthorDto?>
{
    public async Task<AuthorDto?> Handle(GetAuthorByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : AuthorDto.FromModel(model);
    }
}
