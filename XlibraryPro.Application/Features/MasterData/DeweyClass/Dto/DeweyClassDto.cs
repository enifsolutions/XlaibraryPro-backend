using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.DeweyClass.Dto;

public class DeweyClassDto
{
    public long   DeweyId      { get; set; }
    public string DeweyNumber  { get; set; } = string.Empty;
    public string DeweyCaption { get; set; } = string.Empty;

    public static DeweyClassDto FromModel(DeweyClassModel m) => new()
    {
        DeweyId      = m.DeweyId,
        DeweyNumber  = m.DeweyNumber,
        DeweyCaption = m.DeweyCaption
    };
}
