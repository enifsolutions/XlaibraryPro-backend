using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Queries.GetAllMemberStatuses;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.MemberStatus;

public class GetAllMemberStatusesHandlerTests
{
    private readonly Mock<IMemberStatusRepository> _repo = new();
    private readonly GetAllMemberStatusesHandler   _sut;

    public GetAllMemberStatusesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllMemberStatuses()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.MemberStatus(1, "Active"), MasterDataFakes.MemberStatus(2, "Suspended")]);

        var result = await _sut.Handle(new GetAllMemberStatusesQuery(), default);

        result.Should().HaveCount(2);
    }
}