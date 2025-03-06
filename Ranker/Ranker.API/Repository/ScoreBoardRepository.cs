using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Ranker.Lib;
using Ranker.Lib.Models;
using Ranker.Lib.Repository;
using System.Data.SQLite;

namespace Ranker.API.Repository;

public class ScoreBoardRepository : IRepository<ScoreBoard>
{
    private IConnectionHelper<SQLiteConnection> _connectionHelper;
    private const string TABLE = "scoreBoard";

    public ScoreBoardRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<ScoreBoard> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        using SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        SELECT scoreBoardId, name, description, uniqueKey, settingId
        FROM {TABLE}";
        var reader = cmd.ExecuteReader();
        List<ScoreBoard> result = new List<ScoreBoard>();

        while (reader.Read())
        {
            result.Add(new ScoreBoard
            {
                ScoreBoardId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                UniqueKey = reader.GetString(3),
                SettingId = reader.GetInt32(4)
            });
        }

        return result;
    }

	public void DeleteByUniqueKey(string key)
	{

		using SQLiteConnection conn = _connectionHelper.GetConnection();
        using SQLiteCommand cmd = conn.CreateCommand();
		cmd.CommandText = $@"
        PRAGMA foreign_keys = ON;
        DELETE FROM {TABLE}
        WHERE uniqueKey = @uniqueKey";
		cmd.Parameters.AddWithValue("@uniqueKey", key);
		cmd.ExecuteNonQuery();
	}

	public void UpdateByUniqueKey(ScoreBoard model)
    {
		using SQLiteConnection conn = _connectionHelper.GetConnection();
		using SQLiteCommand cmd = conn.CreateCommand();
		cmd.CommandText = $@"
        UPDATE {TABLE}
        SET name = @name, description = @description, uniqueKey = @uniqueKey, settingId = @settingId
        WHERE uniqueKey = @uniqueKey";

		cmd.Parameters.AddWithValue("@name", model.Name);
		cmd.Parameters.AddWithValue("@description", model.Description);
		cmd.Parameters.AddWithValue("@uniqueKey", model.UniqueKey);
		cmd.Parameters.AddWithValue("@settingId", model.SettingId);

		cmd.ExecuteNonQuery();
	}

	public ScoreBoard? GetByUniqueKey(string uniqueKey)
	{
		using SQLiteConnection conn = _connectionHelper.GetConnection();
		using SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        SELECT  scoreBoardId, name, description, uniqueKey, settingId
        FROM {TABLE}
        WHERE uniqueKey = @uniqueKey";

        cmd.Parameters.AddWithValue("@uniqueKey", uniqueKey);
        SQLiteDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			return new ScoreBoard
			{
				ScoreBoardId = reader.GetInt32(0),
				Name = reader.GetString(1),
				Description = reader.GetString(2),
				UniqueKey = reader.GetString(3),
				SettingId = reader.GetInt32(4)
			};
		}
		else
		{
			return null;
		}
	}

	public bool IsUniqueKey(string key)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        using SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        SELECT scoreBoardId
        FROM {TABLE} WHERE uniqueKey = @uniqueKey";

        cmd.Parameters.AddWithValue("uniqueKey", key);
        var reader = cmd.ExecuteReader();

        return !reader.Read();
    }

    public ScoreBoard? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        using SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        SELECT  scoreBoardId, name, description, uniqueKey, settingId
        FROM {TABLE}
        WHERE scoreBoardId = @id";
        cmd.Parameters.AddWithValue("@id", id);

        var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new ScoreBoard
            {
                ScoreBoardId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                UniqueKey = reader.GetString(3),
                SettingId = reader.GetInt32(4)
            };
        }
        else
        {
            return null;
        }
    }

    public void Add(ScoreBoard model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        using SQLiteCommand addCommand = conn.CreateCommand();
		addCommand.CommandText = $@"
        INSERT INTO {TABLE} (name, description, uniqueKey, settingId)
        VALUES (@name, @description, @uniqueKey, @settingId)";

        do
        {
            model.UniqueKey = Utility.CreateScoreboardUniqueKey();
        }
        while (!IsUniqueKey(model.UniqueKey));

		addCommand.Parameters.AddWithValue("@name", model.Name);
		addCommand.Parameters.AddWithValue("@description", model.Description);
		addCommand.Parameters.AddWithValue("@uniqueKey", model.UniqueKey);
		addCommand.Parameters.AddWithValue("@settingId", model.SettingId);

        addCommand.ExecuteNonQuery();

        using SQLiteCommand readCommand = conn.CreateCommand();
        readCommand.CommandText = "SELECT last_insert_rowid()";
        var reader = readCommand.ExecuteReader();
        reader.Read();
        model.ScoreBoardId = reader.GetInt32(0);
    }

    public void Update(ScoreBoard model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        UPDATE {TABLE}
        SET name = @name, description = @description, uniqueKey = @uniqueKey, settingId = @settingId
        WHERE scoreBoardId = @scoreBoardId";

        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@description", model.Description);
        cmd.Parameters.AddWithValue("@uniqueKey", model.UniqueKey);
        cmd.Parameters.AddWithValue("@settingId", model.SettingId);
        cmd.Parameters.AddWithValue("@scoreBoardId", model.ScoreBoardId);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        DELETE FROM {TABLE}
        WHERE scoreBoardId = @scoreBoardId";

        cmd.Parameters.AddWithValue("@scoreBoardId", id);

        cmd.ExecuteNonQuery();
    }
}