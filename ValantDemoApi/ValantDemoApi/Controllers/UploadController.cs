using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ValantDemoApi.Models.UploadMaze;
using ValantDemoApi.Services.Interfaces;

namespace ValantDemoApi.Controllers;

[ApiController]
[Route("Maze/[controller]")]
public sealed class UploadController : ControllerBase
{
  private readonly IUploadMazeService _service;

  public UploadController(IUploadMazeService service)
  {
    _service = service;
  }

  [HttpPost]
  public async Task<bool> UploadMazeFile(UploadMazeRequest request)
  {
    return await _service.UploadMazeAsync(request.FileName, request.MazeFile);
  }
}
