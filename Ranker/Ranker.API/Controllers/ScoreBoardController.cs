using Microsoft.AspNetCore.Mvc;
using Ranker.API.Repository;
using Ranker.Console.Repository;
using Ranker.Lib.Models;

namespace Ranker.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ScoreBoardController : Controller
{
    private readonly ILogger<ScoreBoardController> _logger;

    public ScoreBoardController(ILogger<ScoreBoardController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<ScoreBoard> Get()
    {
        ConsoleConnectionHelper _connHelper = new();
        ScoreBoardRepository repository = new(_connHelper);

        return repository.GetAll().ToArray();
    }

    [HttpGet("single/{uniqueKey}")]
    public ScoreBoard Get(string uniqueKey)
    {
        ConsoleConnectionHelper _connHelper = new();
        ScoreBoardRepository repository = new(_connHelper);

        return repository.GetById(1);
    }

    [HttpPost("create")]
    public ActionResult CreateScoreBoard(ScoreBoard board)
    {
        ConsoleConnectionHelper _connHelper = new();
        ScoreBoardRepository repository = new(_connHelper);
        try
        {
            repository.Add(board);
            return Ok(board);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}
