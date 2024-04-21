namespace ValantDemoApi.Services.Interfaces;

public interface IMazeService
{
  IEnumerable<string> GetAllMazes();
  Task<IEnumerable<string>> GetMazeById(string id);
}
