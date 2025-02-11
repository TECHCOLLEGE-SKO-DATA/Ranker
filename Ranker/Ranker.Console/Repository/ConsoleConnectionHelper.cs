using System.Data.SQLite;
using Ranker.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranker.Console.Repository;

internal class ConsoleConnectionHelper : IConnectionHelper<SQLiteConnection>
{
	public SQLiteConnection GetConnection()
	{
		return new SQLiteConnection("Data Source=database.db");
	}
}
