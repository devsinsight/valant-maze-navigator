using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ValantDemoApi.Models;
using ValantDemoApi.Services;

namespace ValantDemoApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MazeController : ControllerBase
  {
    private readonly ILogger<MazeController> _logger;
    private readonly IMazeService _mazeService;

    public MazeController(ILogger<MazeController> logger, IMazeService mazeService)
    {
      _logger = logger;
      _mazeService = mazeService;
    }

    [HttpGet]
    public IEnumerable<string> GetNextAvailableMoves()
    {
      return _mazeService.GetNextAvailableMoves();
    }

    [HttpPost("upload")]
    public IActionResult UploadMaze([FromBody] MazeUploadRequest request)
    {
      _mazeService.SaveMaze(request.MazeId, request.Grid, request.StartPosition);
      return Ok();
    }

    [HttpPost("navigate")]
    public ActionResult<MazeNavigationResult> Navigate([FromBody] MazeNavigationRequest request)
    {
      try
      {
        var result = _mazeService.Navigate(request.MazeId, request.Direction);
        return Ok(result);
      }
      catch (ArgumentException ex)
      {
        return NotFound(ex.Message);
      }
      catch (InvalidOperationException ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{mazeId}/state")]
    public ActionResult<MazeStateDto> GetMazeState(string mazeId)
    {
      try
      {
        var mazeState = _mazeService.GetMazeState(mazeId);
        return Ok(mazeState);
      }
      catch (ArgumentException ex)
      {
        return NotFound(ex.Message);
      }
    }

  }

  public class MazeUploadRequest
  {
    public string MazeId { get; set; }
    public char[][] Grid { get; set; }
    public Position StartPosition { get; set; }
  }

  public class MazeNavigationRequest
  {
    public string MazeId { get; set; }
    public string Direction { get; set; }
  }
}
