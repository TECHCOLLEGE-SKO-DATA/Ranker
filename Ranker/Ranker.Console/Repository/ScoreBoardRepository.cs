using System.Data.SQLite;
using System.Xml.Linq;
using Ranker.Lib.Models;
using Ranker.Lib.Repository;

namespace Ranker.Console.Repository;

public class ScoreBoardRepository : IRepository<ScoreBoard>
{
	IConnectionHelper<SQLiteConnection> _connectionHelper;
	const string TABLE = "scoreBoard";
	private readonly CommonDBModules<ScoreBoard> _commonDBModules;

	public ScoreBoardRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
	{
		_connectionHelper = connectionHelper;
		_commonDBModules = new(_connectionHelper);
	}

	private ScoreBoard ReadRow(SQLiteDataReader reader)
	{
		return new()
		{
			ScoreBoardId = reader.GetInt32(0),
			Name = reader.GetString(1)
		};
	}

	public IEnumerable<ScoreBoard> GetAll() =>
		_commonDBModules.ExecuteQuery($"SELECT ScoreBoardId, name FROM {TABLE}", ReadRow);

	public ScoreBoard? GetById(int id) =>
		_commonDBModules.ExecuteSingleQuery(@$"SELECT ScoreBoardId, name FROM {TABLE}
		WHERE ScoreBoardId = {id}", ReadRow);

	public void Add(ScoreBoard model) =>
		_commonDBModules.ExecuteNonQuery($@"INSERT INTO {TABLE}(name) VALUES({model.Name})");

	public void Update(ScoreBoard model) =>
		_commonDBModules.ExecuteNonQuery($@"UPDATE {TABLE} SET name = {model.Name}");

	public void Delete(int id) =>
		_commonDBModules.ExecuteNonQuery($@"DELETE FROM {TABLE} WHERE ScoreBoardId = {id}");
}