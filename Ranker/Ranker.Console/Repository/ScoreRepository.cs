using Ranker.Lib.Models;
using Ranker.Lib.Repository;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ranker.Console.Repository;

public class ScoreRepository : IRepository<Score>
{
    private IConnectionHelper<SQLiteConnection> _connectionHelper;
    private const string TABLE = "score";

    public ScoreRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    private string sqlRowsToSelect = "scoreId, participantName, points, scoreBoardId";

    private Score ReadRow(SQLiteDataReader reader)
    {
        return new Score()
        {
            ScoreId = reader.GetInt32(0),
            ScoreBoardId = reader.GetInt32(1),
            ParticipantName = reader.GetString(2),
            Points = reader.GetInt32(3)
        };
    }

    public IEnumerable<Score> GetFromScoreBoardId(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        SELECT {sqlRowsToSelect}
        FROM {TABLE} WHERE scoreBoardId = {id}
        ORDER BY points DESC";

        using SQLiteDataReader reader = cmd.ExecuteReader();
        List<Score> scores = new();

        while (reader.Read())
        {
            scores.Add(new Score
            {
                ScoreId = reader.GetInt32(0),
                ParticipantName = reader.GetString(1),
                Points = reader.GetInt32(2),
                ScoreBoardId = reader.GetInt32(3),
            });
        }

        return scores;
    }

    public void Add(Score model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        INSERT INTO {TABLE} (participantName, points, scoreBoardId)
        VALUES (@participantName, @points, @scoreBoardId)";

        cmd.Parameters.AddWithValue("@participantName", model.ParticipantName);
        cmd.Parameters.AddWithValue("@points", model.Points);
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
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        SELECT {sqlRowsToSelect}
        FROM {TABLE}";

        using SQLiteDataReader reader = cmd.ExecuteReader();
        List<Score> scores = new();

        while (reader.Read())
        {
            scores.Add(new Score
            {
                ScoreId = reader.GetInt32(0),
                ScoreBoardId = reader.GetInt32(1),
                ParticipantName = reader.GetString(2),
                Points = reader.GetInt32(3)
            });
        }

        return scores;
    }

    public Score? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        SELECT {sqlRowsToSelect}
        FROM {TABLE}
        WHERE scoreId = @id";
        cmd.Parameters.AddWithValue("@id", id);

        using SQLiteDataReader reader = cmd.ExecuteReader();
        List<Score> scores = new();

        if (reader.Read())
        {
            return new Score
            {
                ScoreId = reader.GetInt32(0),
                ScoreBoardId = reader.GetInt32(1),
                ParticipantName = reader.GetString(2),
                Points = reader.GetInt32(3)
            };
        }

        return null;
    }

    public void Update(Score model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = $@"
        UPDATE {TABLE}
        SET participantName = @participantName, points = @points
        WHERE scoreId = @scoreId";

        cmd.Parameters.AddWithValue("@participantName", model.ParticipantName);
        cmd.Parameters.AddWithValue("@points", model.Points);
        cmd.Parameters.AddWithValue("@scoreId", model.ScoreId);

        cmd.ExecuteNonQuery();
    }
}