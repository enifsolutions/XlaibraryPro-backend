namespace XlibraryPro.Domain.Models.MasterData;

public class ColourCodeModel
{
    public long   ColourCodeId    { get; set; }
    public string Colour          { get; set; } = string.Empty;
    public int    RotationalOrder { get; set; }
}
