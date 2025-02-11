using System.Data.Common;

namespace Ranker.Lib.Repository;
public interface IConnectionHelper<T>
{
    T GetConnection();
}