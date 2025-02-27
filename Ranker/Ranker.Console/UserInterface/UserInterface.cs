using Ranker.Console.Repository;
using Ranker.Lib.Models;
using System.Text;

namespace Ranker.Console.UserInterface;

internal class UserInterface
{
	ConsoleConnectionHelper _connectionHelper = new();
	ScoreBoardRepository scoreBoardRepo;
	ScoreRepository scoreRepo;
	public UserInterface()
	{
		scoreRepo = new(_connectionHelper);
		scoreBoardRepo = new(_connectionHelper);
	}

    private string Input(string message)
    {
        System.Console.Write(message);
        string? text = System.Console.ReadLine();
        return text != null ? text : "";
    }

	string InputNotWhiteSpace(string message)
	{
		string result = Input(message);
		if (string.IsNullOrWhiteSpace(result))
		{
			throw new Exception();
		}
		return result;
	}

	void DrawScoreBoards()
	{
		System.Console.Clear();
		var data = (List<ScoreBoard>)scoreBoardRepo.GetAll();

		for (int i = 0; i < data.Count(); i++)
		{
			System.Console.WriteLine($"nr.{i + 1} : {data[i].Name}");
		}

		bool exitLoop;
		do
		{
			ScoreBoard scoreBoard;
			int scoreBoardIndex = 0;
			exitLoop = true;
			switch (Input("Choose command: add, delete, update, exit or write number to pick a scoreboard to inspect: ").ToLower())
			{
				case "add":
					CreateScoreBoard();
					break;
				case "delete":
					scoreBoardIndex = GetInputAsInt("Insert what score board you want to delete: ") -1;
					scoreBoard = data[scoreBoardIndex];
					DeleteScoreBoard(scoreBoard.ScoreBoardId);
					break;
				case "update":
					scoreBoardIndex = GetInputAsInt("Insert what score board you want to update: ") - 1;
					scoreBoard = data[scoreBoardIndex];
					UpdateScoreBoard(scoreBoard);
					break;
				case "exit":
					Environment.Exit(0);
					break;
				case string str when int.TryParse(str, out scoreBoardIndex) && scoreBoardIndex <= data.Count() && scoreBoardIndex >= 0:
					DrawScores(data[scoreBoardIndex - 1]);
					break;
				default:
					exitLoop = false;
					continue;
			}
		} while (!exitLoop);
		
	}

    private void DrawScores(ScoreBoard scoreBoard)
    {
        System.Console.Clear();
        string scoreboardText = "-----------------------------------\n";

		List<Score> scores = (List<Score>)scoreRepo.GetFromScoreBoardId(scoreBoard.ScoreBoardId);

		for (int i = 0; i < scores.Count(); i++)
		{
			scoreboardText += $"{i + 1} | {scores[i].ParticipantName} | {scores[i].Points} |\n" +
			"----------------------------------------------------\n";
		}

		System.Console.WriteLine(scoreboardText);
		Score selectedScore;
		int scoreIndex = 0;
		
		switch (Input("Choose command: add, delete, update or exit: ").ToLower())
		{
			case "add":
				CreateScore(scoreBoard);
				break;
			case "delete":
				scoreIndex = GetInputAsInt("Insert what score board you want to update: ") - 1;
				selectedScore = scores[scoreIndex];
				DeleteScore(selectedScore.ScoreId, scoreBoard);
				break;
			case "update":
				scoreIndex = GetInputAsInt("Insert what score board you want to update: ") - 1;
				selectedScore = scores[scoreIndex];
				UpdateScore(selectedScore, scoreBoard);
				break;
			case "exit":
				DrawScoreBoards();
				return;
		}
	}

    public void Draw()
    {
        DrawScoreBoards();
    }

    private void CreateScore(ScoreBoard scoreBoard)
    {
		try
		{
			int points = int.Parse(Input("Insert points: "));
			Score newScore = new Score
			{
				ParticipantName = InputNotWhiteSpace("Insert participant name: "),
				Points = points,
				ScoreBoardId = scoreBoard.ScoreBoardId
			};

			scoreRepo.Add(newScore);

			System.Console.WriteLine("Scoren Blev Tilføjet");
		}
		catch
		{
			System.Console.WriteLine("Insertion failed");
			Thread.Sleep(5000);
		}
		DrawScores(scoreBoard);
    }

	int GetInputAsInt(string message)
	{
		string input;
		int result;
		do
		{
			System.Console.Write(message);
			input = Input(message);
		} while (!int.TryParse(input, out result));

		return result;
	}


    void UpdateScore(Score score, ScoreBoard currentScoreBoard)
    {
		System.Console.Clear();
		try
		{
			score.ParticipantName = InputNotWhiteSpace("Insert participant name: ");
			score.Points = int.Parse(InputNotWhiteSpace("Insert Points: "));
			scoreRepo.Update(score);
		}
		catch
		{
			System.Console.WriteLine("update failed");
			Thread.Sleep(5000);
		}

		DrawScores(currentScoreBoard);
	}

	void DeleteScore(int scoreId, ScoreBoard scoreBoard)
	{
		scoreRepo.Delete(scoreId);

		DrawScores(scoreBoard);
	}

	void CreateScoreBoard()
	{
		System.Console.Clear();
		try
		{
			ScoreBoard scoreBoard = new ScoreBoard()
			{
				Name = InputNotWhiteSpace("name: "),
				Description = InputNotWhiteSpace("description: "),
				UniqueKey = CreatePassword(),
				SettingId = 1,
			};

			scoreBoardRepo.Add(scoreBoard);
		}
		catch
		{
			System.Console.WriteLine("Add failed");
			Thread.Sleep(5000);
		}

		DrawScoreBoards();

	}
	void DeleteScoreBoard(int id)
	{
		scoreBoardRepo.Delete(id);
		DrawScoreBoards();
	}

	void UpdateScoreBoard(ScoreBoard scoreBoard)
	{
		System.Console.Clear();
		try
		{
			scoreBoard.Name = InputNotWhiteSpace("Insert Name: ");
			scoreBoard.Description = InputNotWhiteSpace("Insert description: ");
			scoreBoardRepo.Update(scoreBoard);
		}
		catch
		{
			System.Console.WriteLine("update failed");
			Thread.Sleep(5000);
		}

		DrawScoreBoards();
	}

    public string CreatePassword()
    {
		Random rnd = new Random();
		const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

		while (true)
		{
			int length = 8;
			StringBuilder res = new StringBuilder();
			
			while (0 < length--)
			{
				res.Append(valid[rnd.Next(valid.Length)]);
			}

			string key = res.ToString();
			if (scoreBoardRepo.IsUniqueKey(key))
			{
				return res.ToString();
			}
		}
		
    }
}
