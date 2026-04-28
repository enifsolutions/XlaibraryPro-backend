using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.Publisher.Queries.GetAllPublishers;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.Publisher;

public class GetAllPublishersHandlerTests
{
    private readonly Mock<IPublisherRepository> _repo = new();
    private readonly GetAllPublishersHandler    _sut;

    public GetAllPublishersHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllPublishers()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.Publisher(1, "Oxford UP"), MasterDataFakes.Publisher(2, "Pearson")]);

        var result = await _sut.Handle(new GetAllPublishersQuery(), default);

        result.Should().HaveCount(2);
    }
}