namespace XlibraryPro.Domain.Entities;

/// <summary>
/// Many-to-many join between Book and Genre.
/// Maps to: tblbook_genre_form (book_id FK, genre_form_id FK)
/// </summary>
public class BookGenre
{
    public long BookId  { get; private set; }
    public long GenreId { get; private set; }

    public Book?  Book  { get; private set; }
    public Genre? Genre { get; private set; }

    private BookGenre() { }

    public BookGenre(long bookId, long genreId)
    {
        BookId  = bookId;
        GenreId = genreId;
    }
}