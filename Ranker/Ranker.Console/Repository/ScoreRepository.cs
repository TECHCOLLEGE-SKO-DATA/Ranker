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
		throw new NotImplementedException();
	}

	public void Delete(int id)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Score> GetAll()
	{
		throw new NotImplementedException();
	}

	public Score? GetById(int id)
	{
		throw new NotImplementedException();
	}

	public void Update(Score model)
	{
		throw new NotImplementedException();
	}
}
