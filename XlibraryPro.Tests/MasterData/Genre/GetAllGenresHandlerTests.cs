using FluentAssertions;
using Moq;
using XlibraryPro.Application.Features.MasterData.Genre.Queries.GetAllGenres;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Tests.MasterData.Helpers;

namespace XlibraryPro.Tests.MasterData.Genre;

public class GetAllGenresHandlerTests
{
    private readonly Mock<IGenreRepository> _repo = new();
    private readonly GetAllGenresHandler    _sut;

    public GetAllGenresHandlerTests() => _sut = new(_repo.Object);

    [Fact]
    public async Task Handle_ReturnsAllGenres()
    {
        _repo.Setup(r => r.GetAllAsync(default)).ReturnsAsync([MasterDataFakes.Genre(1, "Fiction"), MasterDataFakes.Genre(2, "Non-Fiction")]);

        var result = await _sut.Handle(new GetAllGenresQuery(), default);

        result.Should().HaveCount(2);
    }
}