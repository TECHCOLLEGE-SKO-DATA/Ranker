using System.Data.SQLite;
using Ranker.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Ranker.Console.Repository;

internal class ConsoleConnectionHelper : IConnectionHelper<SQLiteConnection>
{
	public string DatabaseName => "Ranker.Lib/database.db";
	public SQLiteConnection GetConnection()
	{
		SQLiteConnection conn = new ($"Data Source={DatabaseName}");
		conn.Open();
		return conn;
	}

	public void ExecuteNonQuery(string sql)
	{
		using SQLiteConnection connection = GetConnection();
		SQLiteCommand cmd = connection.CreateCommand();
		cmd.CommandText = sql;
		cmd.ExecuteNonQuery();
	}
}
