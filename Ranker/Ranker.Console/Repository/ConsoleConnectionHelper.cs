using Ranker.Lib.Repository;
using System.Data.SQLite;

namespace Ranker.Console.Repository;

internal class ConsoleConnectionHelper : IConnectionHelper<SQLiteConnection>
{
    public string DatabaseName => "../../../../Ranker.Lib/SQLite/database.db";

    public SQLiteConnection GetConnection()
    {
        SQLiteConnection conn = new($"Data Source={DatabaseName};");
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