using MediatR;
using XlibraryPro.Application.Features.MasterData.Author.Dto;

namespace XlibraryPro.Application.Features.MasterData.Author.Queries.GetAllAuthors;

public record GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>>;
