using MediatR;
using XlibraryPro.Application.Features.MasterData.Author.Dto;

namespace XlibraryPro.Application.Features.MasterData.Author.Queries.GetAuthorById;

public record GetAuthorByIdQuery(long Id) : IRequest<AuthorDto?>;
