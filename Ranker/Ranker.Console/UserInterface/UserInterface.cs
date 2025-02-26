using Ranker.Console.Repository;
using Ranker.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranker.Console.UserInterface;

class UserInterface
{
	ConsoleConnectionHelper _connectionHelper = new();
	ScoreBoardRepository scoreBoardRepo;
	ScoreRepository scoreRepo;

	public UserInterface()
	{
		scoreRepo = new(_connectionHelper);
		scoreBoardRepo = new(_connectionHelper);
	}

	void DrawScoreBoards()
	{

		var data = (List<ScoreBoard>)scoreBoardRepo.GetAll();

		for (int i = 0; i < data.Count(); i++)
		{
			System.Console.WriteLine($"nr.{i} : {data[i].Name}");
		}

		string? userInput = "";
		int userInputNumber = 0;

		do
		{
			System.Console.WriteLine("intast nummer på scoreboard du vil se: ");
			userInput = System.Console.ReadLine();
		} while (userInput == null && int.TryParse(userInput, out userInputNumber) && userInputNumber >= 0 && userInputNumber < data.Count());

		DrawScores(data[userInputNumber]);
	}

	void DrawScores(ScoreBoard scoreBoard)
	{
		System.Console.WriteLine();
		string scoreboardText = "-----------------------------------\n";

		var scores = scoreRepo.GetFromScoreBoardId(scoreBoard.ScoreBoardId);

		foreach (Score score in scores)
		{
			scoreboardText += $"| {score.ParticipantName} | {score.Points} |\n" +
			"----------------------------------------------------\n";

		}

		System.Console.WriteLine(scoreboardText);

		switch (System.Console.ReadLine().ToLower())
		{
			case "add":
				CreateScore(scoreBoard);
				break;
		}
	}

	public void Draw()
	{
		DrawScoreBoards();
	}

    private void CreateScore(ScoreBoard scoreBoard)
    {
        System.Console.WriteLine("Indtast deltagers navn: ");
        string? participantName = System.Console.ReadLine();

        if (string.IsNullOrEmpty(participantName))
        {
            System.Console.WriteLine("Den må ikke være tom");
            return;
        }

        System.Console.WriteLine("Indtast points: ");
        string? pointsInput = System.Console.ReadLine();

		if (string.IsNullOrEmpty(pointsInput) || !int.TryParse(pointsInput, out int points))
        {
            System.Console.WriteLine("Den må ikke være tom");
            return;
        }

		Score newScore = new Score
		{
			ParticipantName = participantName,
			Points = points,
			ScoreBoardId = scoreBoard.ScoreBoardId
		};


        scoreRepo.Add(newScore);

        System.Console.WriteLine("Scoren Blev Tilføjet");
    }




    void UpdateScore()
    {

    }

	void DeleteScore()
	{

	}
}
