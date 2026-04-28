using MediatR;
using XlibraryPro.Application.Features.MasterData.Genre.Dto;

namespace XlibraryPro.Application.Features.MasterData.Genre.Queries.GetGenreById;

public record GetGenreByIdQuery(long Id) : IRequest<GenreDto?>;
