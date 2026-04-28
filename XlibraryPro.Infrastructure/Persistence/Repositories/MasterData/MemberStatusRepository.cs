using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class MemberStatusRepository(IConnectionFactory db) : IMemberStatusRepository
{
    public async Task<IEnumerable<MemberStatusModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<MemberStatusModel>("SELECT * FROM fn_get_all_member_statuses()");
    }

    public async Task<MemberStatusModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<MemberStatusModel>("SELECT * FROM fn_get_member_status(@p_member_status_id)", new { p_member_status_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string status, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",     0,       System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",      id);
        p.Add("p_status",  status);
        p.Add("p_action",  vAction);
        await conn.ExecuteAsync("sp_action_member_status", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
