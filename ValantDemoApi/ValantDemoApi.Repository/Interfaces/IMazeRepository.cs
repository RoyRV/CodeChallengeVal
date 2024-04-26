namespace ValantDemoApi.Repository.Interfaces;

public interface IMazeRepository
{
  Task<bool> UploadMazeAsync(string fileName, List<string> mazeFile);

  IList<string> GetAllMazes();

  Task<IList<string>> GetById(string id);
}
