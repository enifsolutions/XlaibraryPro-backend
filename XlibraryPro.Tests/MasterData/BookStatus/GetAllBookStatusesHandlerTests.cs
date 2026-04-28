using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.BookStatus.Queries.GetAllBookStatuses;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.BookStatus;

public class GetAllBookStatusesHandlerTests
{
    private readonly Mock<IBookStatusRepository> _repo = new();
    private readonly GetAllBookStatusesHandler   _sut;

    public GetAllBookStatusesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllStatuses()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.BookStatus(1, "Available"), MasterDataFakes.BookStatus(2, "Borrowed")]);

        var result = await _sut.Handle(new GetAllBookStatusesQuery(), default);

        result.Should().HaveCount(2);
    }
}