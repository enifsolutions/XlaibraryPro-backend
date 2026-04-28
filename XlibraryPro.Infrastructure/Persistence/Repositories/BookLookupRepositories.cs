using Dapper;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Infrastructure.Configuration;
using XlibraryPro.Infrastructure.Persistence.Models;

namespace XlibraryPro.Infrastructure.Persistence.Repositories;

// ── Genre ─────────────────────────────────────────────────────────────────────
public class GenreRepository(IConnectionFactory db) : IGenreRepository
{
    public async Task<Genre?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Genre>("SELECT * FROM fn_get_genres()");
        return rows.FirstOrDefault(g => g.Id == id);
    }

    public async Task<IEnumerable<Genre>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Genre>("SELECT * FROM fn_get_genres()");
    }

    public async Task AddAsync(Genre genre, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync(
            "INSERT INTO tblGenre_form (genre_form_id, genre_form_name) VALUES (generate_id(), @name)",
            new { name = genre.Name });
    }

    public async Task UpdateAsync(Genre genre, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync(
            "UPDATE tblGenre_form SET genre_form_name = @name WHERE genre_form_id = @id",
            new { name = genre.Name, id = genre.Id });
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM tblGenre_form WHERE genre_form_id = @id", new { id });
    }
}

// ── Publisher ─────────────────────────────────────────────────────────────────
public class PublisherRepository(IConnectionFactory db) : IPublisherRepository
{
    public async Task<Publisher?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Publisher>("SELECT * FROM fn_get_publishers()");
        return rows.FirstOrDefault(p => p.Id == id);
    }

    public async Task<IEnumerable<Publisher>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Publisher>("SELECT * FROM fn_get_publishers()");
    }

    public async Task<IEnumerable<Publisher>> SearchByNameAsync(string name, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Publisher>("SELECT * FROM fn_get_publishers()");
        return rows.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task AddAsync(Publisher publisher, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_publisher(@v_out, @p_publisher_id, @p_publisher_name, @p_action)",
            new { v_out = 0, p_publisher_id = (long?)null, p_publisher_name = publisher.Name, p_action = "ADD" });
        if (result.v_out == 99) throw new Exception("sp_action_publisher returned error on ADD.");
    }

    public async Task UpdateAsync(Publisher publisher, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_publisher(@v_out, @p_publisher_id, @p_publisher_name, @p_action)",
            new { v_out = 0, p_publisher_id = publisher.Id, p_publisher_name = publisher.Name, p_action = "UPDATE" });
        if (result.v_out == 99) throw new Exception("sp_action_publisher returned error on UPDATE.");
    }
}

// ── DeweyClass ────────────────────────────────────────────────────────────────
public class DeweyClassRepository(IConnectionFactory db) : IDeweyClassRepository
{
    public async Task<DeweyClass?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<DeweyClass>("SELECT * FROM fn_get_dewey_classes()");
        return rows.FirstOrDefault(d => d.Id == id);
    }

    public async Task<IEnumerable<DeweyClass>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<DeweyClass>("SELECT * FROM fn_get_dewey_classes()");
    }

    public async Task<IEnumerable<DeweyClass>> SearchAsync(string term, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<DeweyClass>("SELECT * FROM fn_search_dewey(@p_term)", new { p_term = term });
    }

    public async Task AddAsync(DeweyClass deweyClass, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync(
            "INSERT INTO tblDewey_class (dewey_id, dewey_number, dewey_caption) VALUES (generate_id(), @number, @caption)",
            new { number = deweyClass.DeweyNumber, caption = deweyClass.DeweyCaption });
    }
}

// ── Language ──────────────────────────────────────────────────────────────────
public class LanguageRepository(IConnectionFactory db) : ILanguageRepository
{
    public async Task<Language?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Language>("SELECT * FROM fn_get_languages()");
        return rows.FirstOrDefault(l => l.Id == id);
    }

    public async Task<IEnumerable<Language>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Language>("SELECT * FROM fn_get_languages()");
    }

    public async Task AddAsync(Language language, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync(
            "INSERT INTO tblLanguage (language_id, language) VALUES (generate_id(), @name)",
            new { name = language.Name });
    }
}

// ── BookStatus ────────────────────────────────────────────────────────────────
public class BookStatusRepository(IConnectionFactory db) : IBookStatusRepository
{
    public async Task<BookStatus?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<BookStatus>("SELECT * FROM fn_get_book_statuses()");
        return rows.FirstOrDefault(s => s.Id == id);
    }

    public async Task<IEnumerable<BookStatus>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<BookStatus>("SELECT * FROM fn_get_book_statuses()");
    }

    public async Task<BookStatus?> GetByNameAsync(string name, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<BookStatus>("SELECT * FROM fn_get_book_statuses()");
        return rows.FirstOrDefault(s => s.Status.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}