using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class LanguageRepository(IConnectionFactory db) : ILanguageRepository
{
    public async Task<IEnumerable<LanguageModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<LanguageModel>("SELECT * FROM fn_get_all_languages()");
    }

    public async Task<LanguageModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<LanguageModel>("SELECT * FROM fn_get_language(@p_language_id)", new { p_language_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string language, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",       0,        System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",        id);
        p.Add("p_language",  language);
        p.Add("p_action",    vAction);
        await conn.ExecuteAsync("sp_action_language", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
