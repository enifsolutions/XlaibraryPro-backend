using Dapper;
using XlibraryPro.Application.Features.Books.DTOs;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Domain.Models;
using XlibraryPro.Infrastructure.Configuration;
using XlibraryPro.Infrastructure.Persistence.Models;

namespace XlibraryPro.Infrastructure.Persistence.Repositories;

public class BookRepository(IConnectionFactory db) : IBookRepository
{
    // ── READ ─────────────────────────────────────────────────────────────────

    public async Task<Book?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Book>("SELECT * FROM fn_get_book(@p_book_id)", new { p_book_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<Book?> GetByIdWithDetailsAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Book>("SELECT * FROM fn_get_book(@p_book_id)", new { p_book_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<Book?> GetByIsbnAsync(string isbn, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Book>("SELECT * FROM fn_get_books()");
        return rows.FirstOrDefault(b => b.Isbn != null && b.Isbn == isbn);
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Book>("SELECT * FROM fn_get_books_list()");
    }

    public async Task<IEnumerable<Book>> SearchAsync(string searchTerm, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Book>("SELECT * FROM fn_search_books(@p_term)", new { p_term = searchTerm });
    }

    public async Task<IEnumerable<Book>> GetByAuthorAsync(long authorId, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Book>("SELECT * FROM fn_get_books_by_author(@p_author_id)", new { p_author_id = authorId });
    }

    public async Task<IEnumerable<Book>> GetByGenreAsync(long genreId, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Book>("SELECT * FROM fn_get_books_by_genre(@p_genre_id)", new { p_genre_id = genreId });
    }

    public async Task<IEnumerable<Book>> GetByPublisherAsync(long publisherId, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Book>("SELECT * FROM fn_get_books_by_publisher(@p_publisher_id)", new { p_publisher_id = publisherId });
    }

    public async Task<IEnumerable<Book>> GetByDeweyClassAsync(long deweyId, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Book>("SELECT * FROM fn_get_books_by_dewey(@p_dewey_id)", new { p_dewey_id = deweyId });
    }

    public async Task<bool> ExistsAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Book>("SELECT * FROM fn_get_book(@p_book_id)", new { p_book_id = id });
        return rows.Any();
    }

    public async Task<bool> IsbnExistsAsync(string isbn, long? excludeBookId = null, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Book>("SELECT * FROM fn_get_books()");
        return rows.Any(b => b.Isbn != null && b.Isbn == isbn && b.Id != excludeBookId);
    }

    public async Task<IEnumerable<BookAuthorModel>> GetAuthorsAsync(long bookId, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<BookAuthorModel>(
            "SELECT * FROM fn_get_book_authors(@p_book_id)", new { p_book_id = bookId });
    }

    public async Task<IEnumerable<BookGenreModel>> GetGenresAsync(long bookId, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<BookGenreModel>(
            "SELECT * FROM fn_get_book_genres(@p_book_id)", new { p_book_id = bookId });
    }

    public async Task SyncAuthorsAsync(long bookId, IEnumerable<long> authorIds, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync(
            "CALL sp_sync_book_authors(@p_book_id, @p_author_ids)",
            new { p_book_id = bookId, p_author_ids = authorIds.ToArray() });
    }

    public async Task SyncGenresAsync(long bookId, IEnumerable<long> genreIds, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync(
            "CALL sp_sync_book_genres(@p_book_id, @p_genre_ids)",
            new { p_book_id = bookId, p_genre_ids = genreIds.ToArray() });
    }

    // ── WRITE ────────────────────────────────────────────────────────────────

    public async Task<long> AddAsync(Book book, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_book(@v_out, @p_book_id, @p_title, @p_description, @p_isbn, @p_pages, " +
            "@p_primary_language_id, @p_book_type_id, @p_dewey_id, @p_publisher_id, " +
            "@p_place_of_publication, @p_publication_year, @p_copyright_year, " +
            "@p_edition_statement, @p_notes, @p_cover_image_url, @p_action)",
            new
            {
                v_out = 0,
                p_book_id = (long?)null,
                p_title = book.Title,
                p_description = book.Description,
                p_isbn = book.Isbn,
                p_pages = book.Pages,
                p_primary_language_id = book.PrimaryLanguageId,
                p_book_type_id = book.BookTypeId,
                p_dewey_id = book.DeweyId,
                p_publisher_id = book.PublisherId,
                p_place_of_publication = book.PlaceOfPublication,
                p_publication_year = book.PublicationYear,
                p_copyright_year = book.CopyrightYear,
                p_edition_statement = book.EditionStatement,
                p_notes = book.Notes,
                p_cover_image_url = book.CoverImageUrl,
                p_action = "ADD"
            });

        if (result.v_out == 99)
            throw new Exception("sp_action_book returned error on ADD.");

        // Return the newly generated ID from the DB
        using var conn2 = db.CreateConnection();
        return await conn2.QuerySingleAsync<long>(
            "SELECT book_id FROM tblbook ORDER BY book_id DESC LIMIT 1");
    }

    public async Task UpdateAsync(Book book, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_book(@v_out, @p_book_id, @p_title, @p_description , @p_isbn, @p_pages, " +
            "@p_primary_language_id, @p_book_type_id, @p_dewey_id, @p_publisher_id, " +
            "@p_place_of_publication, @p_publication_year, @p_copyright_year, " +
            "@p_edition_statement, @p_notes , @p_cover_image_url, @p_action)",
            new
            {
                v_out                  = 0,
                p_book_id              = book.Id,
                p_title                = book.Title,
                p_description          = book.Description,
                p_isbn                 = book.Isbn,
                p_pages                = book.Pages,
                p_primary_language_id  = book.PrimaryLanguageId,
                p_book_type_id         = book.BookTypeId,
                p_dewey_id             = book.DeweyId,
                p_publisher_id         = book.PublisherId,
                p_place_of_publication = book.PlaceOfPublication,
                p_publication_year     = book.PublicationYear,
                p_copyright_year       = book.CopyrightYear,
                p_edition_statement    = book.EditionStatement,
                p_notes                = book.Notes,
                p_cover_image_url      = book.CoverImageUrl,
                p_action               = "UPDATE"
            });

        if (result.v_out == 99)
            throw new Exception("sp_action_book returned error on UPDATE.");
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_book(@v_out, @p_book_id, @p_title, @p_description , @p_isbn, @p_pages, " +
            "@p_primary_language_id, @p_book_type_id, @p_dewey_id, @p_publisher_id, " +
            "@p_place_of_publication, @p_publication_year, @p_copyright_year, " +
            "@p_edition_statement, @p_notes, @p_action)",
            new
            {
                v_out                  = 0,
                p_book_id              = id,
                p_title                = (string?)null,
                p_description          = (string?)null,
                p_isbn                 = (string?)null,
                p_pages                = (int?)null,
                p_primary_language_id  = (long?)null,
                p_book_type_id         = (long?)null,
                p_dewey_id             = (long?)null,
                p_publisher_id         = (long?)null,
                p_place_of_publication = (string?)null,
                p_publication_year     = (string?)null,
                p_copyright_year       = (int?)null,
                p_edition_statement    = (string?)null,
                p_notes                = (string?)null,
                p_action               = "DELETE"
            });

        if (result.v_out == 99)
            throw new Exception("sp_action_book returned error on DELETE.");
    }
}