using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.ColourCode.Dto;

public class ColourCodeDto
{
    public long   ColourCodeId    { get; set; }
    public string Colour          { get; set; } = string.Empty;
    public int    RotationalOrder { get; set; }

    public static ColourCodeDto FromModel(ColourCodeModel m) => new()
    {
        ColourCodeId    = m.ColourCodeId,
        Colour          = m.Colour,
        RotationalOrder = m.RotationalOrder
    };
}
