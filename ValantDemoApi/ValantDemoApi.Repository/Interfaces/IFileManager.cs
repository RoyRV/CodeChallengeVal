namespace ValantDemoApi.Repository.Interfaces;

internal interface IFileManager
{
  void CreateDirectoryIfDoesNotExists();

  Task<bool> WriteMultiLineFile(string fileName, List<string> fileLines);

  IEnumerable<string> GetFileNames();
}
