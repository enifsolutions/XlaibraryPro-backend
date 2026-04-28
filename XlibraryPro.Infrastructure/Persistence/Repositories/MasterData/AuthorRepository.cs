using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class AuthorRepository(IConnectionFactory db) : IAuthorRepository
{
    public async Task<IEnumerable<AuthorModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<AuthorModel>("SELECT * FROM fn_get_all_authors()");
    }

    public async Task<AuthorModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<AuthorModel>("SELECT * FROM fn_get_author(@p_author_id)", new { p_author_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string firstName, string? middleName, string lastName, string? dates, string? notes, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",         0,          System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",          id);
        p.Add("p_first_name",  firstName);
        p.Add("p_middle_name", middleName);
        p.Add("p_last_name",   lastName);
        p.Add("p_dates",       dates);
        p.Add("p_notes",       notes);
        p.Add("p_action",      vAction);
        await conn.ExecuteAsync("sp_action_author", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
