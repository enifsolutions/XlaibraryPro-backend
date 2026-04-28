namespace XlibraryPro.Application.Features.Books.DTOs;

/// <summary>
/// Represents one row parsed from the uploaded Excel/CSV file.
/// Column names match DB columns exactly.
/// </summary>
public class BookUploadRowDto
{
    public int     RowNumber         { get; set; }
    public string  Title             { get; set; } = string.Empty;
    public string? Discription       { get; set; }   // ERD spelling preserved
    public string? Isbn              { get; set; }
    public int?    Pages             { get; set; }
    public string? Language         { get; set; }
    public string? BookType          { get; set; }
    public string? DeweyNumber       { get; set; }
    public string? PublisherName     { get; set; }
    public string? PlaceOfPublication{ get; set; }
    public string? PublicationYear   { get; set; }
    public int?    CopyrightYear     { get; set; }
    public string? EditionStatement  { get; set; }
    public string? Notes             { get; set; }

    /// <summary>Author full names separated by semicolons. e.g. "Robert Martin;Martin Fowler"</summary>
    public string? Authors           { get; set; }

    /// <summary>Genre names separated by semicolons. e.g. "Software;Programming"</summary>
    public string? Genres            { get; set; }
}

/// <summary>
/// Summary returned to the caller after a bulk upload.
/// </summary>
public class BulkUploadResultDto
{
    public int                    TotalRows  { get; set; }
    public int                    Inserted   { get; set; }
    public int                    Skipped    { get; set; }
    public int                    Failed     { get; set; }
    public List<BulkUploadErrorDto> Errors   { get; set; } = new();
}

public class BulkUploadErrorDto
{
    public int    RowNumber { get; set; }
    public string Title     { get; set; } = string.Empty;
    public string Isbn      { get; set; } = string.Empty;
    public string Reason    { get; set; } = string.Empty;
}