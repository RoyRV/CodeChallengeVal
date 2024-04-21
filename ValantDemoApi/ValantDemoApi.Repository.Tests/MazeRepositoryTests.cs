using FluentAssertions;
using Moq;
using ValantDemoApi.Repository.Interfaces;
using Xunit;

namespace ValantDemoApi.Repository.Tests;

public sealed class MazeRepositoryTests
{
  private readonly Mock<IFileManager> _fileManagerMock;
  private readonly MazeRepository _sut;

  public MazeRepositoryTests()
  {
    // Dependencies
    _fileManagerMock = new Mock<IFileManager>();

    // System under test
    _sut = new(_fileManagerMock.Object);
  }

  [Fact]
  public async Task WhenUploadingMaze_ThenReturnsTrue()
  {
    // Arrange
    var mazeFile = new List<string>();
    var fileName = "fileName.txt";
    _fileManagerMock
      .Setup(mock => mock.WriteMultiLineFile(fileName, mazeFile))
      .ReturnsAsync(true);

    // Act
    var result = await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    result.Should().BeTrue();
  }

  [Fact]
  public async Task GivenFileManagerThrowsException_WhenUploadingMaze_ThenReturnsFalse()
  {
    // Arrange
    var mazeFile = new List<string>();
    var fileName = "fileName.txt";
    _fileManagerMock
      .Setup(mock => mock.WriteMultiLineFile(fileName, mazeFile))
      .ThrowsAsync(new Exception());

    // Act
    var result = await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    result.Should().BeFalse();
  }
}
