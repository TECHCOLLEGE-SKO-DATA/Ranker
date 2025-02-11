using System.Data.SQLite;
using Ranker.Lib.Models;
using Ranker.Lib.Repository;

namespace Ranker.Console.Repository;

public class ScoreBoardRepository : IRepository<ScoreBoard>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;
    const string TABLE = "League";
    public ScoreBoardRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<ScoreBoard> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT PersonId, FirstName, MiddleName, LastName, RegisteredDate, AddressId, PreferredContactMethodId FROM {TABLE}";

        List<ScoreBoard> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            ScoreBoard p = new()
            {
                
            };
            result.Add(p);
        }
        return result;
    }

    public ScoreBoard? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(ScoreBoard model)
    {
        throw new NotImplementedException();
    }

    public void Update(ScoreBoard model)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}