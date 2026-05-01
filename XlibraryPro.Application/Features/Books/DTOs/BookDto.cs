namespace XlibraryPro.Application.Features.Books.DTOs;

public class BookDto
{
    public long BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Isbn { get; set; }
    public int? Pages { get; set; }
    public long PrimaryLanguageId { get; set; }
    public string? LanguageName { get; set; }
    public long BookTypeId { get; set; }
    public string? BookType { get; set; }
    public long DeweyId { get; set; }
    public string? DeweyNumber { get; set; }
    public string? DeweyCaption { get; set; }
    public long PublisherId { get; set; }
    public string? PublisherName { get; set; }
    public string? PublicationYear { get; set; }
    public string? EditionStatement { get; set; }
    public string? Notes { get; set; }
    public int? TotalCopies { get; set; }
    public int? AvailableCopies { get; set; }
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
    public int? TotalCopies            { get; set; }
    public int? AvailableCopies        { get; set; }
    public string? CoverImageUrl        { get; set; }
    public List<BookAuthorDto> Authors { get; set; } = new();
    public List<BookGenreDto> Genres { get; set; } = new();
}

public class BookAuthorDto
{
    public long AuthorId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Role { get; set; }
}

public class BookGenreDto
{
    public long GenreFormId { get; set; }
    public string GenreFormName { get; set; } = string.Empty;
}