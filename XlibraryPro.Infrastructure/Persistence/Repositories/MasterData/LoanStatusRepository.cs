using Dapper;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Domain.Models.MasterData;
using XlibraryPro.Infrastructure.Configuration;

namespace XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;

public class LoanStatusRepository(IConnectionFactory db) : ILoanStatusRepository
{
    public async Task<IEnumerable<LoanStatusModel>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<LoanStatusModel>("SELECT * FROM fn_get_all_loan_statuses()");
    }

    public async Task<LoanStatusModel?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<LoanStatusModel>("SELECT * FROM fn_get_loan_status(@p_loan_status_id)", new { p_loan_status_id = id });
        return rows.FirstOrDefault();
    }

    public async Task<int> ActionAsync(long id, string loanStatus, string vAction, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("v_out",          0,          System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
        p.Add("p_id",           id);
        p.Add("p_loan_status",  loanStatus);
        p.Add("p_action",       vAction);
        await conn.ExecuteAsync("sp_action_loan_status", p, commandType: System.Data.CommandType.StoredProcedure);
        return p.Get<int>("v_out");
    }
}
