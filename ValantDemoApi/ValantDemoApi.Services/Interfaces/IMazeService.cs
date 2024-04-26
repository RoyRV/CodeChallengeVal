namespace ValantDemoApi.Services.Interfaces;

public interface IMazeService
{
  (int, IList<string>) GetAllMazes(int startIndex, int size);
  Task<IList<string>> GetAvailableMoves(string id, int pos);
  Task<IList<string>> GetMazeById(string id);
}
