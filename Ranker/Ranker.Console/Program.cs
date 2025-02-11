// See https://aka.ms/new-console-template for more information
using Ranker.Console.Repository;
using Ranker.Lib.Models;


class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		List<ScoreBoard> boards = new();

		ConsoleConnectionHelper _connHelper = new();
		if (!File.Exists(_connHelper.DatabaseName))
		{
			_connHelper.ExecuteNonQuery(@"CREATE TABLE sdfblasdf(
			ID int jhasdfjklsd
			)");
		}

		ScoreBoardRepository repo = new ScoreBoardRepository(_connHelper);
		////////////////////////
		void getStuff()
		{
			ScoreBoardRepository repo = new ScoreBoardRepository(_connHelper);

		}
	}
}





