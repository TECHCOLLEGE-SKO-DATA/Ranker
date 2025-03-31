using Microsoft.AspNetCore.Mvc;
using Ranker.API.Repository;
using Ranker.Console.Repository;
using Ranker.Lib;
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
        APIConnectionHelper connHelper = new();
        ScoreBoardRepository repository = new(connHelper);

        return repository.GetAll();
    }

    [HttpGet("single{uniqueKey}")]
    public ScoreBoard Get(string uniqueKey)
    {
        APIConnectionHelper connHelper = new();
        ScoreBoardRepository repository = new(connHelper);
        ScoreBoard? scoreBoard = repository.GetByUniqueKey(uniqueKey);

		return scoreBoard == null ? new ScoreBoard() : scoreBoard;
    }

    [HttpDelete("{uniqueKey}")]
    public ActionResult DeleteScoreBoard(string uniqueKey)
	{
		APIConnectionHelper connHelper = new();
		ScoreBoardRepository repository = new(connHelper);

		try
		{
			repository.DeleteByUniqueKey(uniqueKey);
			return Ok();
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}


	[HttpPost]
    public ActionResult CreateScoreBoard([FromBody] ScoreBoard board)
    {
        APIConnectionHelper connHelper = new();
        ScoreBoardRepository repository = new(connHelper);
        board.UniqueKey = Utility.CreateScoreboardUniqueKey();

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

    [HttpPut]
    public ActionResult UpdateScoreBoard([FromBody] ScoreBoard board)
    {
		APIConnectionHelper connHelper = new();
		ScoreBoardRepository repository = new(connHelper);

        try
        {
            repository.UpdateByUniqueKey(board);
            return Ok();
		}
        catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
}
