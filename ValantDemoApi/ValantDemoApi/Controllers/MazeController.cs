using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValantDemoApi.Models.GetMazes;
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

  [HttpPost("/all")]
  public GetMazesResponse GetAllMazes(GetMazesRequest request)
  {
    var (total, items) = _service.GetAllMazes(request.StartIndex, request.Size);
    return new(total, items);
  }

  [HttpGet("/availableMoves")]
  public IEnumerable<string> GetNextAvailableMoves()
  {
    return new List<string> { "Up", "Down", "Left", "Right" };
  }

  [HttpGet("/{id}")]
  public async Task<ActionResult> GetAllMazes(string id)
  {
    var result = await _service.GetMazeById(id);
    return result is null ? NotFound() : Ok(result);
  }
}
