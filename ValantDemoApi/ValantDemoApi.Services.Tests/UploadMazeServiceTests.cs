using FluentAssertions;
using Moq;
using ValantDemoApi.Repository.Interfaces;
using ValantDemoApi.Tests.Shared;
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

  [Theory, AutoMoqData]
  public async Task WhenUploadingMaze_ThenReturnsTrue(string fileName)
  {
    // Arrange
    var mazeFile = GetRandomList();
    fileName = $"{fileName}.txt";
    _mazeRepositoryMock
      .Setup(mock => mock.UploadMazeAsync(fileName, It.IsAny<List<string>>()))
      .ReturnsAsync(true);

    // Act
    var result = await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    result.Should().BeTrue();
  }

  [Theory, AutoMoqData]
  public async Task GivenMazeFileWithDiffLengths_WhenUploadingMaze_ThenCallesRepositoryWithFileWithPadding(string fileName)
  {
    // Arrange
    var mazeFile = GetRandomList();
    fileName = $"{fileName}.txt";
    _mazeRepositoryMock
      .Setup(mock => mock.UploadMazeAsync(fileName, It.IsAny<List<string>>()))
      .ReturnsAsync(true);

    // Act
    await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    var maxLength = mazeFile.Max(x => x.Length);
    var expectedMazeFile = mazeFile.Select(s => s.PadRight(maxLength, 'X').ToUpper()).ToList();
    _mazeRepositoryMock.Verify(mock => mock.UploadMazeAsync(fileName, expectedMazeFile), Times.Once);
  }

  [Fact]
  public async Task GivenRepositoryReturnsFalse_WhenUploadingMaze_ThenReturnsTrue()
  {
    // Arrange
    var mazeFile = GetRandomList();
    var fileName = "fileName.txt";

    // Act
    var result = await _sut.UploadMazeAsync(fileName, mazeFile);

    // Assert
    result.Should().BeFalse();
  }

  private static List<string> GetRandomList()
  {
    return new(){
      StringGenerator.GenerateRandomString(),
      StringGenerator.GenerateRandomString(),
      StringGenerator.GenerateRandomString(),
    };
  }
}
