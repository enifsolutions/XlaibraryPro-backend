using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.ColourCode.Queries.GetAllColourCodes;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.ColourCode;

public class GetAllColourCodesHandlerTests
{
    private readonly Mock<IColourCodeRepository> _repo = new();
    private readonly GetAllColourCodesHandler    _sut;

    public GetAllColourCodesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllColourCodes()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.ColourCode(1, "Red", 1), MasterDataFakes.ColourCode(2, "Blue", 2)]);

        var result = await _sut.Handle(new GetAllColourCodesQuery(), default);

        result.Should().HaveCount(2);
        result.First().Colour.Should().Be("Red");
    }
}