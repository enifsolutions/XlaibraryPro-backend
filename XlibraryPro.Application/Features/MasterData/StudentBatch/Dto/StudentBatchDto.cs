using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.StudentBatch.Dto;

public class StudentBatchDto
{
    public long   StudentBatchId  { get; set; }
    public string SchoolYear      { get; set; } = string.Empty;
    public long   ColourCodeId    { get; set; }
    public int    MaxBooksAllowed { get; set; }

    public static StudentBatchDto FromModel(StudentBatchModel m) => new()
    {
        StudentBatchId  = m.StudentBatchId,
        SchoolYear      = m.SchoolYear,
        ColourCodeId    = m.ColourCodeId,
        MaxBooksAllowed = m.MaxBooksAllowed
    };
}
