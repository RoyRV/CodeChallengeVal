namespace ValantDemoApi.Services.Interfaces;

public interface IMazeService
{
  (int,IEnumerable<string>) GetAllMazes(int startIndex, int size);
  Task<IEnumerable<string>> GetMazeById(string id);
}
