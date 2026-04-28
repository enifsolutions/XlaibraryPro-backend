using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class BookStatusRepository(IConnectionFactory db) : IBookStatusRepository
{
    public async Task<IEnumerable<BookStatusModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<BookStatusModel>("SELECT * FROM fn_get_all_book_statuses()");
    }

    public async Task<BookStatusModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<BookStatusModel>("SELECT * FROM fn_get_book_status(@p_book_status_id)", new { p_book_status_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string bookStatus, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",          0,          System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",           id);
        p.Add("p_book_status",  bookStatus);
        p.Add("p_action",       vAction);
        await conn.ExecuteAsync("sp_action_book_status", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
