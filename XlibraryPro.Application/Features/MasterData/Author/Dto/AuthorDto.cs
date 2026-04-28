using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.Author.Dto;

public class AuthorDto
{
    public long    AuthorId   { get; set; }
    public string  FirstName  { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string  LastName   { get; set; } = string.Empty;
    public string? Dates      { get; set; }
    public string? Notes      { get; set; }

    public static AuthorDto FromModel(AuthorModel m) => new()
    {
        AuthorId   = m.AuthorId,
        FirstName  = m.FirstName,
        MiddleName = m.MiddleName,
        LastName   = m.LastName,
        Dates      = m.Dates,
        Notes      = m.Notes
    };
}
