namespace XlibraryPro.Domain.Entities;

/// <summary>
/// Many-to-many join between Book and Author with a role.
/// Maps to: tblbook_authors (author_id FK, book_id FK, role Short Text)
/// </summary>
public class BookAuthor
{
    public long   BookId   { get; private set; }
    public long   AuthorId { get; private set; }
    public string Role     { get; private set; }

    public Book?   Book   { get; private set; }
    public Author? Author { get; private set; }

    private BookAuthor() { Role = string.Empty; }

    public BookAuthor(long bookId, long authorId, string role = "Author")
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role cannot be empty.", nameof(role));
        BookId   = bookId;
        AuthorId = authorId;
        Role     = role.Trim();
    }

    public void UpdateRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role cannot be empty.", nameof(role));
        Role = role.Trim();
    }
}