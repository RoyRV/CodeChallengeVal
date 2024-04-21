using ValantDemoApi.Repository.Interfaces;
using ValantDemoApi.Services.Interfaces;

namespace ValantDemoApi.Services;

internal sealed class MazeService : IMazeService
{
  private readonly IMazeRepository _repository;

  public MazeService(IMazeRepository repository)
  {
    _repository = repository;
  }

  public IEnumerable<string> GetAllMazes()
  {
    return _repository.GetAllMazes();
  }
}
