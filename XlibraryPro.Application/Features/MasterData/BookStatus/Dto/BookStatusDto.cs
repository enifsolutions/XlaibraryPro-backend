using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.BookStatus.Dto;

public class BookStatusDto
{
    public long   BookStatusId   { get; set; }
    public string BookStatus { get; set; } = string.Empty;

    public static BookStatusDto FromModel(BookStatusModel m) => new()
    {
        BookStatusId   = m.BookStatusId,
        BookStatus = m.BookStatus
    };
}
