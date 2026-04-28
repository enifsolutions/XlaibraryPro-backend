using Dapper;
using XlibraryPro.Application.Common.Interfaces;
using XlibraryPro.Infrastructure.Configuration;
using XlibraryPro.Infrastructure.Persistence.Models;

namespace XlibraryPro.Infrastructure.Services;

/// <summary>
/// Resolves lookup IDs — auto-creates the record if it does not exist.
/// Uses simple in-memory cache per request to avoid redundant DB hits.
/// </summary>
public class BulkBookLookupService(IConnectionFactory db) : IBulkBookLookupService
{
    // Per-request caches
    private readonly Dictionary<string, long> _languageCache   = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, long> _bookTypeCache   = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, long> _publisherCache  = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, long> _deweyCache      = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, long> _genreCache      = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, long> _authorCache     = new(StringComparer.OrdinalIgnoreCase);

    // ── Language ──────────────────────────────────────────────────────────────
    public async Task<long> GetOrCreateLanguageAsync(string name, CancellationToken ct = default)
    {
        if (_languageCache.TryGetValue(name, out var cached)) return cached;

        using var conn = db.CreateConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<long?>(
            "SELECT language_id FROM tblLanguage WHERE LOWER(language) = LOWER(@name)", new { name });

        if (existing.HasValue)
        {
            _languageCache[name] = existing.Value;
            return existing.Value;
        }

        await conn.ExecuteAsync(
            "INSERT INTO tblLanguage (language_id, language) VALUES (generate_id(), @name)", new { name });

        var newId = await conn.QuerySingleAsync<long>(
            "SELECT language_id FROM tblLanguage WHERE LOWER(language) = LOWER(@name)", new { name });

        _languageCache[name] = newId;
        return newId;
    }

    // ── BookType ──────────────────────────────────────────────────────────────
    public async Task<long> GetOrCreateBookTypeAsync(string name, CancellationToken ct = default)
    {
        if (_bookTypeCache.TryGetValue(name, out var cached)) return cached;

        using var conn = db.CreateConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<long?>(
            "SELECT book_type_id FROM tblBook_type WHERE LOWER(book_type) = LOWER(@name)", new { name });

        if (existing.HasValue)
        {
            _bookTypeCache[name] = existing.Value;
            return existing.Value;
        }

        await conn.ExecuteAsync(
            "INSERT INTO tblBook_type (book_type_id, book_type) VALUES (generate_id(), @name)", new { name });

        var newId = await conn.QuerySingleAsync<long>(
            "SELECT book_type_id FROM tblBook_type WHERE LOWER(book_type) = LOWER(@name)", new { name });

        _bookTypeCache[name] = newId;
        return newId;
    }

    // ── Publisher ─────────────────────────────────────────────────────────────
    public async Task<long> GetOrCreatePublisherAsync(string name, CancellationToken ct = default)
    {
        if (_publisherCache.TryGetValue(name, out var cached)) return cached;

        using var conn = db.CreateConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<long?>(
            "SELECT publisher_id FROM tblPublisher WHERE LOWER(publisher_name) = LOWER(@name)", new { name });

        if (existing.HasValue)
        {
            _publisherCache[name] = existing.Value;
            return existing.Value;
        }

        await conn.ExecuteAsync(
            "INSERT INTO tblPublisher (publisher_id, publisher_name) VALUES (generate_id(), @name)", new { name });

        var newId = await conn.QuerySingleAsync<long>(
            "SELECT publisher_id FROM tblPublisher WHERE LOWER(publisher_name) = LOWER(@name)", new { name });

        _publisherCache[name] = newId;
        return newId;
    }

    // ── DeweyClass ────────────────────────────────────────────────────────────
    public async Task<long> GetOrCreateDeweyClassAsync(string number, CancellationToken ct = default)
    {
        if (_deweyCache.TryGetValue(number, out var cached)) return cached;

        using var conn = db.CreateConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<long?>(
            "SELECT dewey_id FROM tblDewey_class WHERE LOWER(dewey_number) = LOWER(@number)", new { number });

        if (existing.HasValue)
        {
            _deweyCache[number] = existing.Value;
            return existing.Value;
        }

        await conn.ExecuteAsync(
            "INSERT INTO tblDewey_class (dewey_id, dewey_number, dewey_caption) VALUES (generate_id(), @number, @number)",
            new { number });

        var newId = await conn.QuerySingleAsync<long>(
            "SELECT dewey_id FROM tblDewey_class WHERE LOWER(dewey_number) = LOWER(@number)", new { number });

        _deweyCache[number] = newId;
        return newId;
    }

    // ── Genre ─────────────────────────────────────────────────────────────────
    public async Task<long> GetOrCreateGenreAsync(string name, CancellationToken ct = default)
    {
        if (_genreCache.TryGetValue(name, out var cached)) return cached;

        using var conn = db.CreateConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<long?>(
            "SELECT genre_form_id FROM tblGenre_form WHERE LOWER(genre_form_name::text) = LOWER(@name)", new { name });

        if (existing.HasValue)
        {
            _genreCache[name] = existing.Value;
            return existing.Value;
        }

        await conn.ExecuteAsync(
            "INSERT INTO tblGenre_form (genre_form_id, genre_form_name) VALUES (generate_id(), @name)", new { name });

        var newId = await conn.QuerySingleAsync<long>(
            "SELECT genre_form_id FROM tblGenre_form WHERE LOWER(genre_form_name::text) = LOWER(@name)", new { name });

        _genreCache[name] = newId;
        return newId;
    }

    // ── Author ────────────────────────────────────────────────────────────────
    public async Task<long?> FindAuthorByNameAsync(string fullName, CancellationToken ct = default)
    {
        if (_authorCache.TryGetValue(fullName, out var cached)) return cached;

        using var conn = db.CreateConnection();
        var id = await conn.QueryFirstOrDefaultAsync<long?>(
            "SELECT author_id FROM tblAuthor WHERE LOWER(first_name || ' ' || last_name) = LOWER(@fullName)",
            new { fullName });

        if (id.HasValue) _authorCache[fullName] = id.Value;
        return id;
    }

    public async Task<long> CreateAuthorAsync(string firstName, string? middleName, string lastName, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync(
            "INSERT INTO tblAuthor (author_id, first_name, middle_name, last_name) VALUES (generate_id(), @firstName, @middleName, @lastName)",
            new { firstName, middleName, lastName });

        var newId = await conn.QuerySingleAsync<long>(
            "SELECT author_id FROM tblAuthor WHERE first_name = @firstName AND last_name = @lastName ORDER BY author_id DESC LIMIT 1",
            new { firstName, lastName });

        var fullName = $"{firstName} {lastName}";
        _authorCache[fullName] = newId;
        return newId;
    }
}