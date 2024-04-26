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

  public async Task<IEnumerable<string>> ReadFile(string fileName)
  {
    var filePath = Path.Combine(CURRENT_DIRECTORY, fileName);
    if (!File.Exists(filePath)) {
      throw new FileNotFoundException();
    }

    using StreamReader reader = new StreamReader(filePath);
    var fileLines = new List<string>();
    string line;

    // Read each line from the file asynchronously and add it to the list
    while ((line = await reader.ReadLineAsync()) != null)
    {
      fileLines.Add(line);
    }

    return fileLines;
  }

}
