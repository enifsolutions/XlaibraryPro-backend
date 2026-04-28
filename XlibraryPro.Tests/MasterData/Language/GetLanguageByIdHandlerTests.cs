using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.Language.Queries.GetLanguageById;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.Language;

public class GetLanguageByIdHandlerTests
{
    private readonly Mock<ILanguageRepository> _repo = new();
    private readonly GetLanguageByIdHandler    _sut;

    public GetLanguageByIdHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsDto_WhenFound()
    {
        _repo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(MasterDataFakes.Language(1, "English"));

        var result = await _sut.Handle(new GetLanguageByIdQuery(1), default);

        result.Should().NotBeNull();
        result!.Language.Should().Be("English");
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenNotFound()
    {
        _repo.Setup(r => r.GetByIdAsync(99, default)).ReturnsAsync((XlibraryPro.Domain.Models.MasterData.LanguageModel?)null);

        var result = await _sut.Handle(new GetLanguageByIdQuery(99), default);

        result.Should().BeNull();
    }
}