using FluentAssertions;
using Moq;
using ValantDemoApi.Repository.Interfaces;
using ValantDemoApi.Tests.Shared;
using Xunit;

namespace ValantDemoApi.Services.Tests;

public sealed class MazeServiceTests
{
  private readonly Mock<IMazeRepository> _mazeRepositoryMock;
  private readonly MazeService _sut;

  public MazeServiceTests()
  {
    // Dependencies
    _mazeRepositoryMock = new Mock<IMazeRepository>();

    // System under test
    _sut = new(_mazeRepositoryMock.Object);
  }

  [Theory]
  [InlineData(0, 5)]
  [InlineData(1, 5)]
  [InlineData(10, 3)]
  public void GivenValidStartIndex_AndSize_WhenGettingAllMazes_ThenPortionOfMazes(
    int startIndex, int size)
  {
    // Arrange
    var mazes = GenerateRandomMazes(100);
    _mazeRepositoryMock.Setup(mock => mock.GetAllMazes()).Returns(mazes);

    // Act
    var (total,items) = _sut.GetAllMazes(startIndex, size);

    // Assert
    items.Should().HaveCount(size);
    items.First().Should().StartWith($"id={startIndex}");
    items.Last().Should().StartWith($"id={(startIndex + size) - 1}");
  }

  [Theory]
  [InlineData(5, 6, 3)]
  [InlineData(1, 6, 3)]
  [InlineData(4, 2, 3)]
  public void GivenInValidStartIndex_OrInvalidSize_WhenGettingAllMazes_ThenPortionOfMazes(
    int startIndex, int size, int numOfItems)
  {
    // Arrange
    var mazes = GenerateRandomMazes(numOfItems);
    _mazeRepositoryMock.Setup(mock => mock.GetAllMazes()).Returns(mazes);

    // Act
    var (total, items) = _sut.GetAllMazes(startIndex, size);

    // Assert
    items.Should().HaveCount(numOfItems);
  }

  [Theory, AutoMoqData]
  public async Task GivenRepositoryReturnsNull_WhenGettingAvailableMoves_ThenReturnsEmpty(
    string id, int position)
  {
    // Arrange
    _mazeRepositoryMock
        .Setup(mock => mock.GetById(id))
        .ReturnsAsync((IList<string>)null);

    // Act
    var result = await _sut.GetAvailableMoves(id, position);

    // Assert
    result.Should().BeEmpty();
  }

  [Theory, AutoMoqData]
  public async Task GivenRepositoryReturnsEmpty_WhenGettingAvailableMoves_ThenReturnsEmpty(
    string id, int position)
  {
    // Arrange
    _mazeRepositoryMock
        .Setup(mock => mock.GetById(id))
        .ReturnsAsync(Enumerable.Empty<string>().ToList());

    // Act
    var result = await _sut.GetAvailableMoves(id, position);

    // Assert
    result.Should().BeEmpty();
  }

  [Theory, AutoMoqData]
  public async Task GivenRepositoryLessThanZeroPosition_WhenGettingAvailableMoves_ThenReturnsEmpty(
    string id, IList<string> list)
  {
    // Arrange
    var position = -1;
    _mazeRepositoryMock
        .Setup(mock => mock.GetById(id))
        .ReturnsAsync(list);

    // Act
    var result = await _sut.GetAvailableMoves(id, position);

    // Assert
    result.Should().BeEmpty();
  }

  [Theory, AutoMoqData]
  public async Task GivenRepositoryInvalidPosition_WhenGettingAvailableMoves_ThenReturnsEmpty(
    string id, IList<string> list)
  {
    // Arrange
    var rows = list.Count();
    var columns = list.Max(x => x.Length);
    var maxIndex = rows * columns ;
    _mazeRepositoryMock
        .Setup(mock => mock.GetById(id))
        .ReturnsAsync(list);

    // Act
    var result = await _sut.GetAvailableMoves(id, maxIndex);

    // Assert
    result.Should().BeEmpty();
  }

  private static List<string> GenerateRandomMazes(int size = 3)
  {
    var list = new List<string>();
    for (var i = 0; i < size; i++)
    {
      list.Add($"id={i}_{StringGenerator.GenerateRandomString()}");
    }
    return list;
  }
}
