using Ranker.Lib.Models;
using Ranker.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranker.Console.Repository;

public class CommonDBModules<T>
{
	private readonly IConnectionHelper<SQLiteConnection> _connectionHelper;

	public CommonDBModules(IConnectionHelper<SQLiteConnection> connectionHelper)
	{
		_connectionHelper = connectionHelper;
	}

	public List<T> ExecuteQuery(string query, Func<SQLiteDataReader, T> readRow)
	{
		using SQLiteConnection conn = _connectionHelper.GetConnection();
		SQLiteCommand command = conn.CreateCommand();
		command.CommandText = query;

		List<T> result = new();
		SQLiteDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			result.Add(readRow(reader));
		}

		return result;
	}

	public void ExecuteNonQuery(string nonQuery)
	{
		using SQLiteConnection conn = _connectionHelper.GetConnection();
		SQLiteCommand command = conn.CreateCommand();
		command.CommandText = nonQuery;

		command.ExecuteNonQuery();
	}

	public T? ExecuteSingleQuery(string query, Func<SQLiteDataReader, T> readRow)
	{
		using SQLiteConnection conn = _connectionHelper.GetConnection();
		SQLiteCommand command = conn.CreateCommand();
		command.CommandText = query;

		List<T> result = new();
		SQLiteDataReader reader = command.ExecuteReader();
		if (reader.Read())
		{
			return readRow(reader);
		}

		return default;
	}
}
