using FluentAssertions;
using Moq;
using ValantDemoApi.Repository.Interfaces;
using Xunit;

namespace ValantDemoApi.Services.Tests;

public sealed class UploadMazeServiceTests
{
  private readonly Mock<IMazeRepository> _mazeRepositoryMock;
  private readonly UploadMazeService _sut;

  public UploadMazeServiceTests()
  {
    // Dependencies
    _mazeRepositoryMock = new Mock<IMazeRepository>();

    // System under test
    _sut = new(_mazeRepositoryMock.Object);
  }

  [Fact]
  public async Task WhenUploadingMaze_ThenReturnsTrue()
  {
    // Arrange
    var mazeFile = new List<string>();
    var fileName = "fileName.txt";
    _mazeRepositoryMock
      .Setup(mock => mock.UploadMazeAsync(fileName, mazeFile))
      .ReturnsAsync(true);

    // Act
    var result = await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    result.Should().BeTrue();
  }

  [Fact]
  public async Task GivenRepositoryReturnsFalse_WhenUploadingMaze_ThenReturnsTrue()
  {
    // Arrange
    var mazeFile = new List<string>();
    var fileName = "fileName.txt";

    // Act
    var result = await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    result.Should().BeFalse();
  }
}
