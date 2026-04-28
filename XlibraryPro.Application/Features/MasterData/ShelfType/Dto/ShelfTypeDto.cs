using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.ShelfType.Dto;

public class ShelfTypeDto
{
    public long   ShelfTypeId   { get; set; }
    public string ShelfType { get; set; } = string.Empty;

    public static ShelfTypeDto FromModel(ShelfTypeModel m) => new()
    {
        ShelfTypeId   = m.ShelfTypeId,
        ShelfType = m.ShelfType
    };
}
