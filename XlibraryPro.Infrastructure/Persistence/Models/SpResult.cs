namespace XlibraryPro.Infrastructure.Persistence.Models;

/// <summary>
/// Maps the INOUT v_out parameter returned by all sp_action_* procedures.
/// 1 = success, 99 = error.
/// </summary>
public class SpResult
{
    public int v_out { get; set; }
}