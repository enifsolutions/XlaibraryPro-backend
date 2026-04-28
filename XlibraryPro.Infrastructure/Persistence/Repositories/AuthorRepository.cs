using Dapper;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Infrastructure.Configuration;
using XlibraryPro.Infrastructure.Persistence.Models;

namespace XlibraryPro.Infrastructure.Persistence.Repositories;

public class AuthorRepository(IConnectionFactory db) : IAuthorRepository
{
    public async Task<Author?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<Author>("SELECT * FROM fn_get_author(@p_author_id)", new { p_author_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Author>("SELECT * FROM fn_get_authors()");
    }

    public async Task<IEnumerable<Author>> SearchByNameAsync(string name, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Author>("SELECT * FROM fn_search_authors(@p_name)", new { p_name = name });
    }

    public async Task AddAsync(Author author, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_author(@v_out, @p_author_id, @p_first_name, @p_middle_name, @p_last_name, @p_dates, @p_notes, @p_action)",
            new
            {
                v_out         = 0,
                p_author_id   = (long?)null,
                p_first_name  = author.FirstName,
                p_middle_name = author.MiddleName,
                p_last_name   = author.LastName,
                p_dates       = author.Dates,
                p_notes       = author.Notes,
                p_action      = "ADD"
            });
        if (result.v_out == 99) throw new Exception("sp_action_author returned error on ADD.");
    }

    public async Task UpdateAsync(Author author, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_author(@v_out, @p_author_id, @p_first_name, @p_middle_name, @p_last_name, @p_dates, @p_notes, @p_action)",
            new
            {
                v_out         = 0,
                p_author_id   = author.Id,
                p_first_name  = author.FirstName,
                p_middle_name = author.MiddleName,
                p_last_name   = author.LastName,
                p_dates       = author.Dates,
                p_notes       = author.Notes,
                p_action      = "UPDATE"
            });
        if (result.v_out == 99) throw new Exception("sp_action_author returned error on UPDATE.");
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_author(@v_out, @p_author_id, @p_first_name, @p_middle_name, @p_last_name, @p_dates, @p_notes, @p_action)",
            new
            {
                v_out         = 0,
                p_author_id   = id,
                p_first_name  = (string?)null,
                p_middle_name = (string?)null,
                p_last_name   = (string?)null,
                p_dates       = (string?)null,
                p_notes       = (string?)null,
                p_action      = "DELETE"
            });
        if (result.v_out == 99) throw new Exception("sp_action_author returned error on DELETE.");
    }
}