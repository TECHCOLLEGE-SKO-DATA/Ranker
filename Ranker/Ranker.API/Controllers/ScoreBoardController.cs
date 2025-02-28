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
}
