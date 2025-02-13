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
            timer REAL NOT NULL,
            scoreBoardId INTEGER,
            FOREIGN KEY (scoreBoardId) REFERENCES scoreBoard(scoreBoardId)
            );");


            
        }

        ScoreBoardRepository repo = new ScoreBoardRepository(_connHelper);

        //repo.Add(new ScoreBoard
        //{
        //    Name = "1",
        //    Description = "2",
        //    UniqueKey = "11111111",
        //    SettingId = 1
            

        //});
        
        var data = repo.GetAll();

        foreach (var item in data)
        {
            Console.WriteLine(item.Name);
        }
    }
}





