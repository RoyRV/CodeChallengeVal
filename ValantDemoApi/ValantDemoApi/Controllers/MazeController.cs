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

  public MazeController(IMazeService service)
  {
    _service= service;
  }

  [HttpPost("/all")]
  public GetMazesResponse GetAllMazes(GetMazesRequest request)
  {
    var (total, items) = _service.GetAllMazes(request.StartIndex, request.Size);
    return new(total, items);
  }

  [HttpGet("/availableMoves/{pos}")]
  public async Task<IEnumerable<string>> GetNextAvailableMovesAsync(string id,int pos)
  {
    return await _service.GetAvailableMoves(id,pos);
  }

  [HttpGet("/maze/{id}")]
  public async Task<IEnumerable<string>> GetMazeById(string id)
  {
    return await _service.GetMazeById(id);
    //return result is null ? NotFound() : Ok(result);
  }
}
