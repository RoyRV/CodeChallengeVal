using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ValantDemoApi.Services.Interfaces;

namespace ValantDemoApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class MazeController : ControllerBase
{
  private readonly IMazeService _service;
  private readonly ILogger<MazeController> _logger;

  public MazeController(IMazeService service,ILogger<MazeController> logger)
  {
    _service= service;
    _logger = logger;
  }

  [HttpGet("/")]
  public IEnumerable<string> GetAllMazes()
  {
    return _service.GetAllMazes();
  }

  [HttpGet("/availableMoves")]
  public IEnumerable<string> GetNextAvailableMoves()
  {
    return new List<string> { "Up", "Down", "Left", "Right" };
  }
}
