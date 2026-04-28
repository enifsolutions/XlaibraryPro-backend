using MediatR;
using XlibraryPro.Application.Features.MasterData.BookType.Dto;

namespace XlibraryPro.Application.Features.MasterData.BookType.Queries.GetAllBookTypes;

public record GetAllBookTypesQuery : IRequest<IEnumerable<BookTypeDto>>;
