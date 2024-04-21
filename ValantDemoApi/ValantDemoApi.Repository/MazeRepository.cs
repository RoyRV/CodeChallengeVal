using ValantDemoApi.Repository.Interfaces;

namespace ValantDemoApi.Repository;

internal sealed class MazeRepository : IMazeRepository
{
  private readonly IFileManager _fileManager;

  public MazeRepository(IFileManager fileManager)
  {
    _fileManager = fileManager;
    _fileManager.CreateDirectoryIfDoesNotExists();
  }

  public async Task<bool> UploadMazeAsync(string fileName, List<string> mazeFile)
  {
    try
    {
      return await _fileManager.WriteMultiLineFile(fileName, mazeFile);
    }
    catch (Exception e)
    {
      Console.WriteLine("An error occurred: " + e.Message);
      return false;
    }
  }

  public IEnumerable<string> GetAllMazes()
  {
    return _fileManager.GetFileNames();
  }
}
