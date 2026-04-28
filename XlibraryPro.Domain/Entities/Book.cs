using XlibraryPro.Domain.Common;
using XlibraryPro.Domain.ValueObjects;
using XlibraryPro.Domain.Enums;

namespace XlibraryPro.Domain.Entities;

/// <summary>
/// Core bibliographic record — Aggregate Root of the Books module.
/// Maps to: tblbook
/// </summary>
public class Book : BaseEntity, IAuditableEntity
{
    public string  Title       { get; private set; }
    public string? Description { get; private set; }
    public Isbn?   Isbn        { get; private set; }
    public int?    Pages       { get; private set; }

    public long PrimaryLanguageId { get; private set; }
    public long BookTypeId        { get; private set; }
    public long DeweyId           { get; private set; }
    public long PublisherId       { get; private set; }

    public string? PlaceOfPublication { get; private set; }
    public string? PublicationYear    { get; private set; }
    public int?    CopyrightYear      { get; private set; }
    public string? EditionStatement   { get; private set; }
    public string? Notes              { get; private set; }

    public DateTime  CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<BookAuthor> _authors = new();
    public IReadOnlyCollection<BookAuthor> Authors => _authors.AsReadOnly();

    private readonly List<BookGenre> _genres = new();
    public IReadOnlyCollection<BookGenre> Genres => _genres.AsReadOnly();

    private readonly List<BookCopy> _copies = new();
    public IReadOnlyCollection<BookCopy> Copies => _copies.AsReadOnly();

    public Language?      PrimaryLanguage { get; private set; }
    public BookTypeEntity? BookType       { get; private set; }
    public DeweyClass?    DeweyClass      { get; private set; }
    public Publisher?     Publisher       { get; private set; }

    private Book() : base() { Title = string.Empty; }

    public Book(long id, string title, long primaryLanguageId, long bookTypeId,
                long deweyId, long publisherId, string? isbn = null,
                string? description = null, int? pages = null,
                string? placeOfPublication = null, string? publicationYear = null,
                int? copyrightYear = null, string? editionStatement = null,
                string? notes = null)
        : base(id)
    {
        SetTitle(title);
        SetForeignKeys(primaryLanguageId, bookTypeId, deweyId, publisherId);
        SetIsbn(isbn);
        UpdateDetails(description, pages, placeOfPublication, publicationYear,
                      copyrightYear, editionStatement, notes);
        CreatedAt = DateTime.UtcNow;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Book title cannot be empty.", nameof(title));
        Title = title.Trim();
        Touch();
    }

    public void SetIsbn(string? isbn)
    {
        Isbn = ValueObjects.Isbn.TryCreate(isbn);
        Touch();
    }

    public void SetForeignKeys(long languageId, long bookTypeId, long deweyId, long publisherId)
    {
        if (languageId  <= 0) throw new ArgumentException("Invalid language ID.",    nameof(languageId));
        if (bookTypeId  <= 0) throw new ArgumentException("Invalid book type ID.",   nameof(bookTypeId));
        if (deweyId     <= 0) throw new ArgumentException("Invalid Dewey class ID.", nameof(deweyId));
        if (publisherId <= 0) throw new ArgumentException("Invalid publisher ID.",   nameof(publisherId));
        PrimaryLanguageId = languageId;
        BookTypeId        = bookTypeId;
        DeweyId           = deweyId;
        PublisherId       = publisherId;
        Touch();
    }

    public void UpdateDetails(string? description, int? pages, string? placeOfPublication,
                              string? publicationYear, int? copyrightYear,
                              string? editionStatement, string? notes)
    {
        if (pages is < 0)
            throw new ArgumentException("Pages cannot be negative.", nameof(pages));
        if (copyrightYear is < 1000 or > 9999)
            throw new ArgumentException("Copyright year must be a 4-digit year.", nameof(copyrightYear));

        Description        = description?.Trim();
        Pages              = pages;
        PlaceOfPublication = placeOfPublication?.Trim();
        PublicationYear    = publicationYear?.Trim();
        CopyrightYear      = copyrightYear;
        EditionStatement   = editionStatement?.Trim();
        Notes              = notes?.Trim();
        Touch();
    }

    public void AddAuthor(long authorId, string role = "Author")
    {
        if (_authors.Any(a => a.AuthorId == authorId && a.Role == role)) return;
        _authors.Add(new BookAuthor(Id, authorId, role));
    }

    public void RemoveAuthor(long authorId, string role = "Author")
    {
        var entry = _authors.FirstOrDefault(a => a.AuthorId == authorId && a.Role == role);
        if (entry != null) _authors.Remove(entry);
    }

    public void AddGenre(long genreId)
    {
        if (_genres.Any(g => g.GenreId == genreId)) return;
        _genres.Add(new BookGenre(Id, genreId));
    }

    public void RemoveGenre(long genreId)
    {
        var entry = _genres.FirstOrDefault(g => g.GenreId == genreId);
        if (entry != null) _genres.Remove(entry);
    }

    public BookCopy AddCopy(long copyId, string barcode, long bookStatusId, long shelfTypeId,
                            string shelfLocation, BookCondition condition,
                            DateTime acquisitionDate, string? notes = null)
    {
        if (_copies.Any(c => c.Barcode == barcode))
            throw new InvalidOperationException($"A copy with barcode '{barcode}' already exists.");

        var copy = new BookCopy(copyId, Id, barcode, bookStatusId, shelfTypeId,
                                shelfLocation, condition, acquisitionDate, notes);
        _copies.Add(copy);
        return copy;
    }

    public int TotalCopies     => _copies.Count;
    public int AvailableCopies => _copies.Count(c => c.IsAvailable);

    private void Touch() { if (Id > 0) UpdatedAt = DateTime.UtcNow; }

    public override string ToString() => $"[{Id}] {Title}";
}
