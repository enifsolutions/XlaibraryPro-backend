using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class DeweyClassRepository(IConnectionFactory db) : IDeweyClassRepository
{
    public async Task<IEnumerable<DeweyClassModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<DeweyClassModel>("SELECT * FROM fn_get_all_dewey_classes()");
    }

    public async Task<DeweyClassModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<DeweyClassModel>("SELECT * FROM fn_get_dewey_class(@p_dewey_id)", new { p_dewey_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string deweyNumber, string deweyCaption, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",           0,            System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",            id);
        p.Add("p_dewey_number",  deweyNumber);
        p.Add("p_dewey_caption", deweyCaption);
        p.Add("p_action",        vAction);
        await conn.ExecuteAsync("sp_action_dewey_class", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
