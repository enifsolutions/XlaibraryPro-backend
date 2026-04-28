using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.Author.Queries.GetAllAuthors;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.Author;

public class GetAllAuthorsHandlerTests
{
    private readonly Mock<IAuthorRepository> _repo = new();
    private readonly GetAllAuthorsHandler    _sut;

    public GetAllAuthorsHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllAuthors()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.Author(1, "Arthur", "Clarke"), MasterDataFakes.Author(2, "Chinua", "Achebe")]);

        var result = await _sut.Handle(new GetAllAuthorsQuery(), default);

        result.Should().HaveCount(2);
        result.First().LastName.Should().Be("Clarke");
    }
}