using ValantDemoApi.Repository.Interfaces;
using ValantDemoApi.Services.Interfaces;

namespace ValantDemoApi.Services;

internal sealed class UploadMazeService : IUploadMazeService
{
  private readonly IMazeRepository _repository;

  public UploadMazeService(IMazeRepository repository)
  {
    _repository = repository;
  }

  public async Task<bool> UploadMazeAsync(string fileName, List<string> mazeFile)
  {
    var maxLength = mazeFile.Max(x => x.Length);
    // Completing strings than has lower length than maximum with X
    mazeFile = mazeFile.Select(s => s.PadRight(maxLength, 'X').ToUpper()).ToList();
    return await _repository.UploadMazeAsync(fileName,mazeFile);
  }

  public IEnumerable<string> GetAllMazes()
  {
    return _repository.GetAllMazes();
  }
}
