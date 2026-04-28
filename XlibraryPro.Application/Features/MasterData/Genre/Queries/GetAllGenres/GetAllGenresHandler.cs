using MediatR;
using XlibraryPro.Application.Features.MasterData.Genre.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Genre.Queries.GetAllGenres;

public class GetAllGenresHandler(IGenreRepository repo) : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreDto>>
{
    public async Task<IEnumerable<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken ct)
        => (await repo.GetAllAsync(ct)).Select(GenreDto.FromModel);
}
