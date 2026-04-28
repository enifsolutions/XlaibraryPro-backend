using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class GenreRepository(IConnectionFactory db) : IGenreRepository
{
    public async Task<IEnumerable<GenreModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<GenreModel>("SELECT * FROM fn_get_all_genres()");
    }

    public async Task<GenreModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<GenreModel>("SELECT * FROM fn_get_genre(@p_genre_form_id)", new { p_genre_form_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string genreFormName, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",             0,             System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",              id);
        p.Add("p_genre_form_name", genreFormName);
        p.Add("p_action",          vAction);
        await conn.ExecuteAsync("sp_action_genre", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
