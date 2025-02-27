// See https://aka.ms/new-console-template for more information
using Ranker.Console.Repository;
using Ranker.Console.UserInterface;
using System.Data.SQLite;

internal class Program
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
            uniqueKey TEXT NOT NULL UNIQUE CHECK(LENGTH(username) = 8),
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

        UserInterface userInterface = new UserInterface();
        userInterface.Draw();
    }
}