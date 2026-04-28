using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.Language.Dto;

public class LanguageDto
{
    public long   LanguageId   { get; set; }
    public string Language { get; set; } = string.Empty;

    public static LanguageDto FromModel(LanguageModel m) => new()
    {
        LanguageId   = m.LanguageId,
        Language = m.Language
    };
}
