using XlibraryPro.Domain.Common;

namespace XlibraryPro.Domain.Entities;

/// <summary>
/// Represents an author/contributor.
/// Maps to: tblAuthor (author_id PK, first_name, middle_name, last_name, dates, notes)
/// </summary>
public class Author : BaseEntity, IAuditableEntity
{
    public string  FirstName  { get; private set; }
    public string? MiddleName { get; private set; }
    public string  LastName   { get; private set; }
    public string? Dates      { get; private set; }
    public string? Notes      { get; private set; }

    public DateTime  CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<BookAuthor> _bookAuthors = new();
    public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.AsReadOnly();

    private Author() : base() { FirstName = string.Empty; LastName = string.Empty; }

    public Author(long id, string firstName, string lastName,
                  string? middleName = null, string? dates = null, string? notes = null)
        : base(id)
    {
        SetName(firstName, lastName, middleName);
        Dates     = dates?.Trim();
        Notes     = notes?.Trim();
        CreatedAt = DateTime.UtcNow;
    }

    public void SetName(string firstName, string lastName, string? middleName = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Author first name cannot be empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Author last name cannot be empty.", nameof(lastName));

        FirstName  = firstName.Trim();
        LastName   = lastName.Trim();
        MiddleName = middleName?.Trim();
        UpdatedAt  = DateTime.UtcNow;
    }

    public void UpdateDetails(string? dates, string? notes)
    {
        Dates     = dates?.Trim();
        Notes     = notes?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public string FullName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";

    public string CatalogName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{LastName}, {FirstName}"
        : $"{LastName}, {FirstName} {MiddleName}";

    public override string ToString() => FullName;
}