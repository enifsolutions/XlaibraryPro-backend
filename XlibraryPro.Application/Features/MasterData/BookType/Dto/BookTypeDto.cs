using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.BookType.Dto;

public class BookTypeDto
{
    public long   BookTypeId   { get; set; }
    public string BookType { get; set; } = string.Empty;

    public static BookTypeDto FromModel(BookTypeModel m) => new()
    {
        BookTypeId   = m.BookTypeId,
        BookType = m.BookType
    };
}
