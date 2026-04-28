using MediatR;
using XlibraryPro.Application.Features.MasterData.Genre.Dto;

namespace XlibraryPro.Application.Features.MasterData.Genre.Queries.GetAllGenres;

public record GetAllGenresQuery : IRequest<IEnumerable<GenreDto>>;
