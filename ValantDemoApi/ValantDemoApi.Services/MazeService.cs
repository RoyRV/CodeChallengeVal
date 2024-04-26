using ValantDemoApi.Repository.Interfaces;
using ValantDemoApi.Services.Interfaces;
using ValantDemoApi.Shared;

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

  public (int, IList<string>) GetAllMazes(int startIndex, int size)
  {
    var mazes = _repository.GetAllMazes();
    if (!mazes.Any())
    {
      return (0, Enumerable.Empty<string>().ToList());
    }

    var numOfItems = mazes.Count;
    if (numOfItems < size || startIndex > numOfItems)
    {
      startIndex = DEFAULT_START_INDEX;
      size = DEFAULT_SIZE;
    }
    return (numOfItems, mazes.Skip(startIndex).Take(size).ToList());
  }

  public async Task<IList<string>> GetMazeById(string id)
  {
    return await _repository.GetById(id);
  }

  public async Task<IList<string>> GetAvailableMoves(string id, int position)
  {
    var maze = await _repository.GetById(id);
    // Check if maze is valid 
    if (maze is null || !maze.Any())
    {
      return Enumerable.Empty<string>().ToList();
    }

    var rows = maze.Count();
    var columns = maze.Max(x => x.Length);
    var maxIndex = rows * columns-1;

    // check if position is valid
    if (position < 0 || position > maxIndex)
    {
      return Enumerable.Empty<string>().ToList();
    }

    var availableMoves = new List<string>();
    var columnIndex = position % columns;
    var rowIndex = position / columns;

    // Check if UP is possible
    if (rowIndex > 0 &&
      maze[rowIndex - 1][columnIndex] != ValantConstants.BLOCKER_CHAR)
    {
      availableMoves.Add(ValantConstants.UP);
    }

    // Check if DOWN is possible
    if (rowIndex < rows - 1 &&
      maze[rowIndex + 1][columnIndex] != ValantConstants.BLOCKER_CHAR)
    {
      availableMoves.Add(ValantConstants.DOWN);
    }

    // Check if LEFT is possible
    if (columnIndex > 0 &&
      maze[rowIndex][columnIndex - 1] != ValantConstants.BLOCKER_CHAR)
    {
      availableMoves.Add(ValantConstants.LEFT);
    }

    // Check if RIGHT is possible
    if (columnIndex < columns - 1 &&
      maze[rowIndex][columnIndex + 1] != ValantConstants.BLOCKER_CHAR)
    {
      availableMoves.Add(ValantConstants.RIGHT);
    }

    return availableMoves;
  }

}
