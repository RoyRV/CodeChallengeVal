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
