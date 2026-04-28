using XlibraryPro.Domain.Common;

namespace XlibraryPro.Domain.Entities;

/// <summary>
/// Represents a literary genre or form.
/// Maps to: tblGenre_form (genre_form_id PK, genre_form_name)
/// </summary>
public class Genre : BaseEntity
{
    public string Name { get; private set; }

    private readonly List<BookGenre> _bookGenres = new();
    public IReadOnlyCollection<BookGenre> BookGenres => _bookGenres.AsReadOnly();

    private Genre() : base() { Name = string.Empty; }

    public Genre(long id, string name) : base(id) { SetName(name); }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Genre name cannot be empty.", nameof(name));
        Name = name.Trim();
    }

    public override string ToString() => Name;
}