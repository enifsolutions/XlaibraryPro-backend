using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.Language.Queries.GetAllLanguages;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.Language;

public class GetAllLanguagesHandlerTests
{
    private readonly Mock<ILanguageRepository> _repo = new();
    private readonly GetAllLanguagesHandler    _sut;

    public GetAllLanguagesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllLanguages()
    {
        var models = new[] { MasterDataFakes.Language(1, "English"), MasterDataFakes.Language(2, "Sinhala") };
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync(models);

        var result = await _sut.Handle(new GetAllLanguagesQuery(), default);

        result.Should().HaveCount(2);
        result.First().Language.Should().Be("English");
    }

    [Fact]
    public async Task Handle_ReturnsEmpty_WhenNoLanguages()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([]);

        var result = await _sut.Handle(new GetAllLanguagesQuery(), default);

        result.Should().BeEmpty();
    }
}