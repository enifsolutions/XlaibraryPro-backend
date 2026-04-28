using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class StudentBatchRepository(IConnectionFactory db) : IStudentBatchRepository
{
    public async Task<IEnumerable<StudentBatchModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<StudentBatchModel>("SELECT * FROM fn_get_all_student_batches()");
    }

    public async Task<StudentBatchModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<StudentBatchModel>("SELECT * FROM fn_get_student_batch(@p_student_batch_id)", new { p_student_batch_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string schoolYear, long colourCodeId, int maxBooksAllowed, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",                0,               System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",                 id);
        p.Add("p_school_year",        schoolYear);
        p.Add("p_colour_code_id",     colourCodeId);
        p.Add("p_max_books_allowed",  maxBooksAllowed);
        p.Add("p_action",             vAction);
        await conn.ExecuteAsync("sp_action_student_batch", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
