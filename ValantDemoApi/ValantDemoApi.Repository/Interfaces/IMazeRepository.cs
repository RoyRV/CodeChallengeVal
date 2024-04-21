namespace ValantDemoApi.Repository.Interfaces;

public interface IMazeRepository
{
  Task<bool> UploadMazeAsync(string fileName, List<string> mazeFile);

  IEnumerable<string> GetAllMazes();
  Task<IEnumerable<string>> GetById(string id);
}
