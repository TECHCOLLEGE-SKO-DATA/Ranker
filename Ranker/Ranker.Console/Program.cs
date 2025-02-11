// See https://aka.ms/new-console-template for more information
using Ranker.Console.Repository;
using Ranker.Lib.Models;

Console.WriteLine("Hello, World!");
List<ScoreBoard> boards = new();

ConsoleConnectionHelper _connHelper = new();

ScoreBoardRepository repo = new ScoreBoardRepository(_connHelper);
////////////////////////
void getStuff()
{
	ScoreBoardRepository repo = new ScoreBoardRepository(_connHelper);

}



