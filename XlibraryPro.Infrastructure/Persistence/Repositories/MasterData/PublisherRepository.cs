using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class PublisherRepository(IConnectionFactory db) : IPublisherRepository
{
    public async Task<IEnumerable<PublisherModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<PublisherModel>("SELECT * FROM fn_get_all_publishers()");
    }

    public async Task<PublisherModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<PublisherModel>("SELECT * FROM fn_get_publisher(@p_publisher_id)", new { p_publisher_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string publisherName, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",            0,             System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",             id);
        p.Add("p_publisher_name", publisherName);
        p.Add("p_action",         vAction);
        await conn.ExecuteAsync("sp_action_publisher", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
