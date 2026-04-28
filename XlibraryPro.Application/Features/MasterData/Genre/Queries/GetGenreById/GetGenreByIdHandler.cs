using MediatR;
using XlibraryPro.Application.Features.MasterData.Genre.Dto;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Application.Features.MasterData.Genre.Queries.GetGenreById;

public class GetGenreByIdHandler(IGenreRepository repo) : IRequestHandler<GetGenreByIdQuery, GenreDto?>
{
    public async Task<GenreDto?> Handle(GetGenreByIdQuery request, CancellationToken ct)
    {
        var model = await repo.GetByIdAsync(request.Id, ct);
        return model is null ? null : GenreDto.FromModel(model);
    }
}
