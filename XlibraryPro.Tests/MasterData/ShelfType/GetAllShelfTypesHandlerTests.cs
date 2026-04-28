using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.ShelfType.Queries.GetAllShelfTypes;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.ShelfType;

public class GetAllShelfTypesHandlerTests
{
    private readonly Mock<IShelfTypeRepository> _repo = new();
    private readonly GetAllShelfTypesHandler    _sut;

    public GetAllShelfTypesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllShelfTypes()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.ShelfType(1, "Standard"), MasterDataFakes.ShelfType(2, "Reference")]);

        var result = await _sut.Handle(new GetAllShelfTypesQuery(), default);

        result.Should().HaveCount(2);
    }
}