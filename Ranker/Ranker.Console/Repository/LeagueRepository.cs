using Ranker.Lib.Models;
using Ranker.Lib.Repository;

namespace Ranker.Console.Repository;

public class LeagueRepository : IRepository<League>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;
    const string TABLE = "League";
    public LeagueRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<League> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT PersonId, FirstName, MiddleName, LastName, RegisteredDate, AddressId, PreferredContactMethodId FROM {TABLE}";

        List<PerLeagueson> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            League p = new()
            {
                PersonId = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                MiddleName = reader.GetString(2),
                LastName = reader.GetString(3),
                RegisterdDate = reader.GetDateTime(4),
                Address = reader.GetInt32(5),
                PreferredContactMethod = reader.GetInt32(6),
            };
            result.Add(p);
        }
        return result;
    }

    public League? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(League model)
    {
        throw new NotImplementedException();
    }

    public void Update(League model)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}