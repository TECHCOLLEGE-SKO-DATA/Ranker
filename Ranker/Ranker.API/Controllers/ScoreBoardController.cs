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
        APIConnectionHelper _connHelper = new();
        ScoreBoardRepository repository = new(_connHelper);

        return repository.GetAll();
    }

    [HttpGet("single{uniqueKey}")]
    public ScoreBoard Get(string uniqueKey)
    {
        APIConnectionHelper _connHelper = new();
        ScoreBoardRepository repository = new(_connHelper);
        ScoreBoard? scoreBoard = repository.GetByUniqueKey(uniqueKey);

		return scoreBoard == null ? new ScoreBoard() : scoreBoard;
    }

    [HttpDelete("{uniqueKey}")]
    public ActionResult DeleteScoreBoard(string uniqueKey)
	{
		APIConnectionHelper _connHelper = new();
		ScoreBoardRepository repository = new(_connHelper);

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
        APIConnectionHelper _connHelper = new();
        ScoreBoardRepository repository = new(_connHelper);
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
		APIConnectionHelper _connHelper = new();
		ScoreBoardRepository repository = new(_connHelper);

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
