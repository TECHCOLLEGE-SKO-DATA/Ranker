using Ranker.Console.Repository;
using Ranker.Lib.Models;
using System.Text;

namespace Ranker.Console.UserInterface;

internal class UserInterface
{
    private ConsoleConnectionHelper _connectionHelper = new();
    private ScoreBoardRepository scoreBoardRepo;
    private ScoreRepository scoreRepo;

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

    private void DrawScoreBoards()
    {
        System.Console.Clear();
        var data = (List<ScoreBoard>)scoreBoardRepo.GetAll();

        for (int i = 0; i < data.Count(); i++)
        {
            System.Console.WriteLine($"nr.{i} : {data[i].Name}");
        }

        bool exitLoop;
        do
        {
            exitLoop = true;
            switch (Input("Choose command: add, delete, update, exit or write number to pick a scoreboard to inspect").ToLower())
            {
                case "add":
                    CreateScoreBoard();
                    break;

                case string str when int.TryParse(str, out int scoreBoardIndex) && scoreBoardIndex < data.Count() && scoreBoardIndex >= 0:
                    DrawScores(data[scoreBoardIndex]);
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

        var scores = scoreRepo.GetFromScoreBoardId(scoreBoard.ScoreBoardId);

        foreach (Score score in scores)
        {
            scoreboardText += $"| {score.ParticipantName} | {score.Points} |\n" +
            "----------------------------------------------------\n";
        }

        System.Console.WriteLine(scoreboardText);

        switch (Input("").ToLower())
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

        if (string.IsNullOrWhiteSpace(participantName))
        {
            System.Console.WriteLine("Den må ikke være tom");
            return;
        }

        System.Console.WriteLine("Indtast points: ");
        string? pointsInput = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(pointsInput) || !int.TryParse(pointsInput, out int points))
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

    private void UpdateScore()
    {
    }

    private void DeleteScore()
    {
    }

    private void CreateScoreBoard()
    {
        System.Console.Clear();
        string name = Input("name: ");
        string description = Input("description: ");

        scoreBoardRepo.Add(new ScoreBoard()
        {
            Name = name,
            Description = description,
        });
    }

    public string CreatePassword()
    {
        int length = 8;
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }

    private void DeleteScoreBoard()
    {
    }
}