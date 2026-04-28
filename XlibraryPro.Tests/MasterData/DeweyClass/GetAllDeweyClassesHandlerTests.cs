using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Queries.GetAllDeweyClasses;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.DeweyClass;

public class GetAllDeweyClassesHandlerTests
{
    private readonly Mock<IDeweyClassRepository> _repo = new();
    private readonly GetAllDeweyClassesHandler   _sut;

    public GetAllDeweyClassesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllDeweyClasses()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.DeweyClass(1, "000", "Computer Science"), MasterDataFakes.DeweyClass(2, "100", "Philosophy")]);

        var result = await _sut.Handle(new GetAllDeweyClassesQuery(), default);

        result.Should().HaveCount(2);
        result.First().DeweyNumber.Should().Be("000");
    }
}