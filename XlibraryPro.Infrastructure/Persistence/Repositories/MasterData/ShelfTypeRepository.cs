using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class ShelfTypeRepository(IConnectionFactory db) : IShelfTypeRepository
{
    public async Task<IEnumerable<ShelfTypeModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<ShelfTypeModel>("SELECT * FROM fn_get_all_shelf_types()");
    }

    public async Task<ShelfTypeModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<ShelfTypeModel>("SELECT * FROM fn_get_shelf_type(@p_shelf_type_id)", new { p_shelf_type_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string shelfType, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",        0,         System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",         id);
        p.Add("p_shelf_type", shelfType);
        p.Add("p_action",     vAction);
        await conn.ExecuteAsync("sp_action_shelf_type", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
