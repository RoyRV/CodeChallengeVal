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

  public IList<string> GetAllMazes()
  {
    return _fileManager.GetFileNames().ToList();
  }

  public async Task<IList<string>> GetById(string fileName)
  {
    try
    {
      return (await _fileManager.ReadFile(fileName)).ToList();
    }
    catch (FileNotFoundException)
    {
      Console.WriteLine($"file {fileName} does not exists");
      return null;
    }
    catch (Exception e)
    {
      Console.WriteLine($"Error trying to read file: {fileName} - Exception :{e.Message}");
      throw;
    }
  }
}
