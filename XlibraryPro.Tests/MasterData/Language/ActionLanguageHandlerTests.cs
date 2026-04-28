using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.Language.Commands.ActionLanguage;
using XlibraryPro.Domain.Interfaces.IMasterData;

namespace XlibraryPro.Tests.MasterData.Language;

public class ActionLanguageHandlerTests
{
    private readonly Mock<ILanguageRepository> _repo = new();
    private readonly ActionLanguageHandler     _sut;

    public ActionLanguageHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_Add_ReturnsOne_OnSuccess()
    {
        _repo.Setup(r => r.ActionAsync(0, "Tamil", "ADD", default)).ReturnsAsync(1);

        var result = await _sut.Handle(new ActionLanguageCommand(0, "ADD", "Tamil"), default);

        result.Should().Be(1);
    }

    [Fact]
    public async Task Handle_Update_ReturnsOne_OnSuccess()
    {
        _repo.Setup(r => r.ActionAsync(1, "English (UK)", "UPDATE", default)).ReturnsAsync(1);

        var result = await _sut.Handle(new ActionLanguageCommand(1, "UPDATE", "English (UK)"), default);

        result.Should().Be(1);
    }

    [Fact]
    public async Task Handle_ReturnsNinetyNine_OnFailure()
    {
        _repo.Setup(r => r.ActionAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), default)).ReturnsAsync(99);

        var result = await _sut.Handle(new ActionLanguageCommand(0, "ADD", "Tamil"), default);

        result.Should().Be(99);
    }
}