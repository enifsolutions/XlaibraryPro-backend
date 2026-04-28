using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.Genre.Dto;

public class GenreDto
{
    public long   GenreFormId   { get; set; }
    public string GenreFormName { get; set; } = string.Empty;

    public static GenreDto FromModel(GenreModel m) => new()
    {
        GenreFormId   = m.GenreFormId,
        GenreFormName = m.GenreFormName
    };
}
