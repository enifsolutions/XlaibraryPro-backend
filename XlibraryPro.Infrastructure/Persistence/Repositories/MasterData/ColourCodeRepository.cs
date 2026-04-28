using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class ColourCodeRepository(IConnectionFactory db) : IColourCodeRepository
{
    public async Task<IEnumerable<ColourCodeModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<ColourCodeModel>("SELECT * FROM fn_get_all_colour_codes()");
    }

    public async Task<ColourCodeModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<ColourCodeModel>("SELECT * FROM fn_get_colour_code(@p_colour_code_id)", new { p_colour_code_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string colour, int rotationalOrder, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",              0,              System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",               id);
        p.Add("p_colour",           colour);
        p.Add("p_rotational_order", rotationalOrder);
        p.Add("p_action",           vAction);
        await conn.ExecuteAsync("sp_action_colour_code", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
