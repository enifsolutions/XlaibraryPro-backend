using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Queries.GetAllStudentBatches;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.StudentBatch;

public class GetAllStudentBatchesHandlerTests
{
    private readonly Mock<IStudentBatchRepository> _repo = new();
    private readonly GetAllStudentBatchesHandler   _sut;

    public GetAllStudentBatchesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllBatches()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.StudentBatch(1, "2024", 1, 5), MasterDataFakes.StudentBatch(2, "2025", 2, 5)]);

        var result = await _sut.Handle(new GetAllStudentBatchesQuery(), default);

        result.Should().HaveCount(2);
        result.First().SchoolYear.Should().Be("2024");
    }
}