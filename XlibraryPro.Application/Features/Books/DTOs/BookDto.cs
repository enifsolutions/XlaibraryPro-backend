namespace XlibraryPro.Application.Features.Books.DTOs;

public class BookDto
{
    public long    BookId            { get; set; }
    public string  Title             { get; set; } = string.Empty;
    public string? Isbn              { get; set; }
    public int?    Pages             { get; set; }
    public string? LanguageName      { get; set; }
    public string? BookType          { get; set; }
    public string? DeweyNumber       { get; set; }
    public string? DeweyCaption      { get; set; }
    public string? PublisherName     { get; set; }
    public string? PublicationYear   { get; set; }
    public string? EditionStatement  { get; set; }
}

public class BookDetailDto
{
    public long    BookId              { get; set; }
    public string  Title               { get; set; } = string.Empty;
    public string? Description         { get; set; }
    public string? Isbn                { get; set; }
    public int?    Pages               { get; set; }
    public long    PrimaryLanguageId   { get; set; }
    public string? LanguageName        { get; set; }
    public long    BookTypeId          { get; set; }
    public string? BookType            { get; set; }
    public long    DeweyId             { get; set; }
    public string? DeweyNumber         { get; set; }
    public string? DeweyCaption        { get; set; }
    public long    PublisherId         { get; set; }
    public string? PublisherName       { get; set; }
    public string? PlaceOfPublication  { get; set; }
    public string? PublicationYear     { get; set; }
    public int?    CopyrightYear       { get; set; }
    public string? EditionStatement    { get; set; }
    public string? Notes               { get; set; }
}