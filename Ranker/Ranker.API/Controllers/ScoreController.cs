using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ranker.API.Repository;
using Ranker.Console.Repository;
using Ranker.Lib.Models;
using System.Security.AccessControl;

namespace Ranker.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ScoreController : ControllerBase
{
	private readonly ILogger<ScoreController> _logger;

	public ScoreController(ILogger<ScoreController> logger)
	{
		_logger = logger;
	}

	[HttpGet]
	public IEnumerable<Score> Get()
	{
		APIConnectionHelper connHelper = new();
		ScoreRepository repository = new(connHelper);

		return repository.GetAll();
	}

	[HttpGet("getById/{id}")]
	public Score GetById(int id)
	{
		APIConnectionHelper connHelper = new();
		ScoreRepository repository = new(connHelper);
		Score? score = repository.GetById(id);

		return score == null ? new Score() : score;
	}

	[HttpPost]
	public ActionResult Create([FromBody]Score score)
	{
		APIConnectionHelper connHelper = new();
		ScoreRepository repository = new(connHelper);

		try
		{
			repository.Add(score);
			return Ok(score);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public ActionResult Delete(int id)
	{
		APIConnectionHelper connHelper = new();
		ScoreRepository repository = new(connHelper);

		try
		{
			repository.Delete(id);
			return Ok();
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}

	[HttpPut]
	public ActionResult Put([FromBody] Score score)
	{
		APIConnectionHelper connHelper = new();
		ScoreRepository repository = new(connHelper);

		try
		{
			repository.Update(score);
			return Ok(score);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("getFromScoreBoardId/{scoreBoardId}")]
	public IEnumerable<Score> GetFromScoreBoardId(int scoreBoardId)
	{

		APIConnectionHelper connHelper = new();
		ScoreRepository repository = new(connHelper);

		return repository.GetFromScoreBoardId(scoreBoardId);
	}
}
