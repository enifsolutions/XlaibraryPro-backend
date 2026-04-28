namespace XlibraryPro.Domain.Models.MasterData;

public class StudentBatchModel
{
    public long   StudentBatchId  { get; set; }
    public string SchoolYear      { get; set; } = string.Empty;
    public long   ColourCodeId    { get; set; }
    public int    MaxBooksAllowed { get; set; }
}
