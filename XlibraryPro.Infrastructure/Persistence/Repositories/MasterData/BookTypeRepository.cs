using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class BookTypeRepository(IConnectionFactory db) : IBookTypeRepository
{
    public async Task<IEnumerable<BookTypeModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<BookTypeModel>("SELECT * FROM fn_get_all_book_types()");
    }

    public async Task<BookTypeModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<BookTypeModel>("SELECT * FROM fn_get_book_type(@p_book_type_id)", new { p_book_type_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string bookType, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",        0,        System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",         id);
        p.Add("p_book_type",  bookType);
        p.Add("p_action",     vAction);
        await conn.ExecuteAsync("sp_action_book_type", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
