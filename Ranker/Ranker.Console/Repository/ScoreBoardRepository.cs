using System.Data.SQLite;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using Ranker.Lib.Models;
using Ranker.Lib.Repository;

namespace Ranker.Console.Repository;

public class ScoreBoardRepository : IRepository<ScoreBoard>
{
	IConnectionHelper<SQLiteConnection> _connectionHelper;
	const string TABLE = "scoreBoard";

	public ScoreBoardRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
	{
		_connectionHelper = connectionHelper;
	}

    public IEnumerable<ScoreBoard> GetAll()
    {
		using SQLiteConnection conn = _connectionHelper.GetConnection();
		SQLiteCommand cmd = conn.CreateCommand();
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

    public ScoreBoard? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
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
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        INSERT INTO {TABLE} (name, description, uniqueKey, settingId) 
        VALUES (@name, @description, @uniqueKey, @settingId)";

        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@description", model.Description);
        cmd.Parameters.AddWithValue("@uniqueKey", model.UniqueKey);
        cmd.Parameters.AddWithValue("@settingId", model.SettingId);

        cmd.ExecuteNonQuery();
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





