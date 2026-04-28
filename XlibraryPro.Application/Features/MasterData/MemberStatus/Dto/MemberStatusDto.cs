using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.MemberStatus.Dto;

public class MemberStatusDto
{
    public long   MemberStatusId   { get; set; }
    public string Status { get; set; } = string.Empty;

    public static MemberStatusDto FromModel(MemberStatusModel m) => new()
    {
        MemberStatusId   = m.MemberStatusId,
        Status = m.Status
    };
}
