namespace XlibraryPro.Domain.Models.MasterData;

public class AuthorModel
{
    public long    AuthorId   { get; set; }
    public string  FirstName  { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string  LastName   { get; set; } = string.Empty;
    public string? Dates      { get; set; }
    public string? Notes      { get; set; }
}
