using FluentAssertions;
using Moq;
using ValantDemoApi.Repository.Interfaces;
using ValantDemoApi.Tests.Shared;
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

  [Theory, AutoMoqData]
  public async Task WhenUploadingMaze_ThenReturnsTrue(string fileName, List<string> mazeFile)
  {
    // Arrange
    _fileManagerMock
      .Setup(mock => mock.WriteMultiLineFile(fileName, mazeFile))
      .ReturnsAsync(true);

    // Act
    var result = await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    result.Should().BeTrue();
  }

  [Theory, AutoMoqData]
  public async Task GivenFileManagerThrowsException_WhenUploadingMaze_ThenReturnsFalse(
    string fileName, List<string> mazeFile, Exception exception)
  {
    // Arrange
    _fileManagerMock
      .Setup(mock => mock.WriteMultiLineFile(fileName, mazeFile))
      .ThrowsAsync(exception);

    // Act
    var result = await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    result.Should().BeFalse();
  }
}
