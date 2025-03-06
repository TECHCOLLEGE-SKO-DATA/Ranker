using Ranker.Lib.Repository;
using System.Data.SQLite;

namespace Ranker.Console.Repository;

internal class APIConnectionHelper : IConnectionHelper<SQLiteConnection>
{
    public string DatabaseName => @"..\..\Ranker.Lib\SQLite/database.db";
    public string CommandSetting => "PRAGMA foreign_keys = ON;";


	public SQLiteConnection GetConnection()
    {
        string noget = Environment.CurrentDirectory + DatabaseName;
        if (File.Exists(noget))
        {
            SQLiteConnection conn = new($"Data Source={noget};");

            conn.Open();
            return conn;
        }
        
        return null;
    }

    public void ExecuteNonQuery(string sql)
    {
        using SQLiteConnection connection = GetConnection();
        SQLiteCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql; //PouchDB
        cmd.ExecuteNonQuery();
    }
}