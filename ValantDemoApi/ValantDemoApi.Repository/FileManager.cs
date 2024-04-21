using ValantDemoApi.Repository.Interfaces;

namespace ValantDemoApi.Repository;

internal sealed class FileManager : IFileManager
{
  private readonly string CURRENT_DIRECTORY = Environment.CurrentDirectory + "\\mazes\\";

  public void CreateDirectoryIfDoesNotExists()
  {
    if (!Directory.Exists(CURRENT_DIRECTORY))
    {
      Directory.CreateDirectory(CURRENT_DIRECTORY);
    }
  }

  public IEnumerable<string> GetFileNames()
  {
    return Directory.GetFiles(CURRENT_DIRECTORY)
            .Select(file => Path.GetFileName(file));
  }

  public async Task<bool> WriteMultiLineFile(string fileName, List<string> fileLines)
  {
    var filePath = Path.Combine(CURRENT_DIRECTORY, fileName);
    using var sw = new StreamWriter(filePath);

    foreach (string str in fileLines)
    {
      await sw.WriteLineAsync(str.ToUpper());
    }

    Console.WriteLine("Strings have been written to the file.");
    return true;
  }
}
