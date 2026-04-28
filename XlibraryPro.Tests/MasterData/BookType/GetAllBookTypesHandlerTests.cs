using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.BookType.Queries.GetAllBookTypes;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.BookType;

public class GetAllBookTypesHandlerTests
{
    private readonly Mock<IBookTypeRepository> _repo = new();
    private readonly GetAllBookTypesHandler    _sut;

    public GetAllBookTypesHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllBookTypes()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.BookType(1, "General"), MasterDataFakes.BookType(2, "Reference")]);

        var result = await _sut.Handle(new GetAllBookTypesQuery(), default);

        result.Should().HaveCount(2);
    }
}