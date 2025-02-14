using Ranker.Lib.Models;
using Ranker.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranker.Console.Repository;

public class ScoreRepository : IRepository<Score>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;
    const string TABLE = "score";
    private readonly CommonDBModules<Score> _commonDBModules;

    public ScoreRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
        _commonDBModules = new(_connectionHelper);
    }

    private Score ReadRow(SQLiteDataReader reader)
    {
        return new()
        {
            ScoreId = reader.GetInt32(0),
            ScoreBoardId = reader.GetInt32(1),
            ParticipantName = reader.GetString(2),
            Points = reader.GetInt32(3),
            Timer = reader.GetFloat(4)
        };
    }

    public void Add(Score model)
	{
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        INSERT INTO {TABLE} (participantName, points, timer, scoreBoardId) 
        VALUES (@participantName, @points, @timer, @scoreBoardId)";

        cmd.Parameters.AddWithValue("@participantName", model.ParticipantName);
        cmd.Parameters.AddWithValue("@points", model.Points);
        cmd.Parameters.AddWithValue("@timer", model.Timer);
        cmd.Parameters.AddWithValue("@scoreBoardId", model.ScoreBoardId);

        cmd.ExecuteNonQuery();
    }

	public void Delete(int id)
	{
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        DELETE FROM {TABLE} 
        WHERE scoreId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

	public IEnumerable<Score> GetAll()
	{
        return _commonDBModules.ExecuteQuery($@" 
        SELECT scoreId, participantName, points, timer, scoreBoardId
        FROM {TABLE}", ReadRow);
    }

	public Score? GetById(int id)
	{
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        SELECT scoreId, participantName, points, timer, scoreBoardId
        FROM {TABLE}
        WHERE scoreId = @id";
        cmd.Parameters.AddWithValue("@id", id);

        return _commonDBModules.ExecuteSingleQuery(cmd, ReadRow);
    }

	public void Update(Score model)
	{
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        UPDATE {TABLE} 
        SET participantName = @participantName, points = @points, timer = @timer
        WHERE scoreId = @scoreId";


        cmd.Parameters.AddWithValue("@participantName", model.ParticipantName);
        cmd.Parameters.AddWithValue("@points", model.Points);
        cmd.Parameters.AddWithValue("@timer", model.Timer);
        cmd.Parameters.AddWithValue("@scoreId", model.ScoreId);

        cmd.ExecuteNonQuery();
    }
}
