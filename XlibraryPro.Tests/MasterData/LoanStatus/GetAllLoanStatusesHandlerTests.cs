using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Queries.GetAllLoanStatuses;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.LoanStatus;

public class GetAllLoanStatusesHandlerTests
{
    private readonly Mock<ILoanStatusRepository> _repo = new();
    private readonly GetAllLoanStatusesHandler   _sut;

    public GetAllLoanStatusesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllLoanStatuses()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.LoanStatus(1, "Active"), MasterDataFakes.LoanStatus(2, "Returned")]);

        var result = await _sut.Handle(new GetAllLoanStatusesQuery(), default);

        result.Should().HaveCount(2);
    }
}