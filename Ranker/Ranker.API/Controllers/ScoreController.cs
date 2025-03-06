using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ranker.API.Repository;
using Ranker.Console.Repository;
using Ranker.Lib.Models;
using System.Security.AccessControl;

namespace Ranker.API.Controllers;

[Route("api/[controller]")]
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
		APIConnectionHelper _connHelper = new();
		ScoreRepository repository = new(_connHelper);

		return repository.GetAll();
	}

	[HttpGet("getById/{id}")]
	public Score GetById(int id)
	{
		APIConnectionHelper _connHelper = new();
		ScoreRepository repository = new(_connHelper);
		Score? score = repository.GetById(id);

		return score == null ? new Score() : score;
	}

	[HttpPost("{score}")]
	public ActionResult Create(Score score)
	{
		APIConnectionHelper _connHelper = new();
		ScoreRepository repository = new(_connHelper);

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
		APIConnectionHelper _connHelper = new();
		ScoreRepository repository = new(_connHelper);

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

	[HttpPut("{score}")]
	public ActionResult Put(Score score)
	{
		APIConnectionHelper _connHelper = new();
		ScoreRepository repository = new(_connHelper);

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

	[HttpGet("getFromScoreBoardId{scoreBoardId}")]
	public IEnumerable<Score> GetFromScoreBoardId(int scoreBoardId)
	{

		APIConnectionHelper _connHelper = new();
		ScoreRepository repository = new(_connHelper);

		return repository.GetFromScoreBoardId(scoreBoardId);
	}
}
