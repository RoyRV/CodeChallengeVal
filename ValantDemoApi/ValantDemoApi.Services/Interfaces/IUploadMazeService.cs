namespace ValantDemoApi.Services.Interfaces;

public interface IUploadMazeService
{
  Task<bool> UploadMazeAsync(string fileName, List<string> mazeFile);
}
