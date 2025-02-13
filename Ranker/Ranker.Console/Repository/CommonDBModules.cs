using Ranker.Lib.Models;
using Ranker.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

		return ReadAll(reader, readRow);
	}

	private List<T> ReadAll(SQLiteDataReader reader, Func<SQLiteDataReader, T> readRow)
	{
        List<T> result = new();

        while (reader.Read())
        {
            result.Add(readRow(reader));
        }

        return result;
    }
	
	public SQLiteCommand CreateCommand(string query, SQLiteConnection conn)
	{
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = query;
		return command;
    }

    public List<T> ExecuteQuery(SQLiteCommand command, Func<SQLiteDataReader, T> readRow)
	{
		return ReadAll(command.ExecuteReader(), readRow);
	}


    public void ExecuteNonQuery(string nonQuery)
	{
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        CreateCommand(nonQuery, conn).ExecuteNonQuery();
	}

	public T? ExecuteSingleQuery(string query, Func<SQLiteDataReader, T> readRow)
	{
		using SQLiteConnection conn = _connectionHelper.GetConnection();
		SQLiteCommand command = CreateCommand(query, conn);

		SQLiteDataReader reader = command.ExecuteReader();

		return ReadSingleRow(reader, readRow);
	}

    public T? ExecuteSingleQuery(SQLiteCommand command, Func<SQLiteDataReader, T> readRow)
	{
        SQLiteDataReader reader = command.ExecuteReader();

        return ReadSingleRow(reader, readRow);
    }



    private T? ReadSingleRow(SQLiteDataReader reader, Func<SQLiteDataReader, T> readRow)
	{
        if (reader.Read())
        {
            return readRow(reader);
        }

        return default;
    }
}
