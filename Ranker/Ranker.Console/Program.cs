// See https://aka.ms/new-console-template for more information
using Ranker.Console.Repository;
using Ranker.Lib.Models;
using System.Data.SQLite;


class Program
{
    public static void Main(string[] args)
    {

        ConsoleConnectionHelper _connHelper = new();
        if (!File.Exists(_connHelper.DatabaseName))
        {
            SQLiteConnection.CreateFile(_connHelper.DatabaseName);

            // _connHelper.ExecuteNonQuery("DROP TABLE IF EXISTS scoreBoard;");
            _connHelper.ExecuteNonQuery(@"CREATE TABLE scoreBoard (
            scoreBoardId INTEGER PRIMARY KEY AUTOINCREMENT, 
            name TEXT NOT NULL, 
            description TEXT NOT NULL, 
            uniqueKey TEXT NOT NULL UNIQUE, 
            settingId INTEGER NOT NULL
            );");

            //_connHelper.ExecuteNonQuery("DROP TABLE IF EXISTS score;");
            _connHelper.ExecuteNonQuery(@"CREATE TABLE score (
            scoreId INTEGER PRIMARY KEY AUTOINCREMENT, 
            participantName TEXT NOT NULL, 
            points INTEGER NOT NULL, 
            scoreBoardId INTEGER,
            FOREIGN KEY (scoreBoardId) REFERENCES scoreBoard(scoreBoardId)
            );");
        }

        ScoreBoardRepository scoreBoardRepo = new(_connHelper);
		ScoreRepository scoreRepo = new(_connHelper);
        try
        {
			scoreBoardRepo.Add(new ScoreBoard
			{
				Name = "1",
				Description = "2",
				UniqueKey = "11111111",
				SettingId = 1
			});
		}
        catch
        {

        }

        try
        {
			scoreRepo.Add(new()
			{
				Points = 5,
				ParticipantName = "Shaq",
				ScoreBoardId = 1,
			});
		}
        catch
        {

        }
        

        var data = (List<ScoreBoard>)scoreBoardRepo.GetAll();

        for (int i = 0; i < data.Count(); i++)
        {
            Console.WriteLine($"nr.{i} : {data[i].Name}");
        }

        string? userInput = "";
        int userInputNumber = 0;

        do
        {
            Console.Write("intast nummer på scoreboard du vil se: ");
            userInput = Console.ReadLine();
        } while (userInput == null && int.TryParse(userInput, out userInputNumber) && userInputNumber >= 0 && userInputNumber < data.Count());

        ScoreBoard pickedScoreboard = data[userInputNumber];

        Console.Clear();
        string scoreboardText = "-----------------------------------\n";

        var scores = scoreRepo.GetFromScoreBoardId(pickedScoreboard.ScoreBoardId);

        foreach (Score score in scores)
        {
            scoreboardText += $"| {score.ParticipantName} | {score.Points} |\n" +
            "----------------------------------------------------\n";
            
        }

        Console.WriteLine(scoreboardText);

        Console.ReadLine();
    }
}





