using ValantDemoApi.Repository.Interfaces;
using ValantDemoApi.Services.Interfaces;

namespace ValantDemoApi.Services;

internal sealed class MazeService : IMazeService
{
  // DEFAULT PAGINATION VALUES
  private const int DEFAULT_START_INDEX = 0;
  private const int DEFAULT_SIZE = 25;
  private readonly IMazeRepository _repository;

  public MazeService(IMazeRepository repository)
  {
    _repository = repository;
  }

  public (int, IEnumerable<string>) GetAllMazes(int startIndex, int size)
  {
    var mazes = _repository.GetAllMazes();
    if (!mazes.Any())
    {
      return (0, Enumerable.Empty<string>());
    }

    var numOfItems = mazes.Count();
    if (numOfItems < size || startIndex > numOfItems)
    {
      startIndex = DEFAULT_START_INDEX;
      size = DEFAULT_SIZE;
    }
    return (numOfItems, mazes.Skip(startIndex).Take(size));
  }

  public async Task<IEnumerable<string>> GetMazeById(string id)
  {
    return await _repository.GetById(id);
  }
}
